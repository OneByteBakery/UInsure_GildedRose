using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using Autofac;

using GildedRose.Autofac;
using GildedRose.Items;

namespace GildedRoseKata
{
    internal static class Program
    {
        /// <summary>
        /// Simulates the passage of time over the requested number of days, producing a statement of depreciating quality over time to the console standard output stream.
        /// </summary>
        /// <param name="args">Requires exactly one argument which represents a valid <see cref="uint"/> value (e.g. 30)</param>
        /// <exception cref="ArgumentException">When the supplied args contains an incorrect number of values, or a non-positive integer</exception>
        internal static void Main([NotNull] string[] args)
        {
            if (args?.Length != 1)
            {
                throw new ArgumentException($"Exactly one argument must be supplied. Received {args?.Length ?? 0}");
            }

            if (!uint.TryParse(args[0], out var numberOfDays))
            {
                throw new ArgumentException("Supplied argument must be a positive integer");
            }
            
            IList<Item> items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new AppreciatingItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new VelbenItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new VelbenItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new VelbenItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            var container = BuildContainer();
            var app = container.Resolve<GildedRose>(TypedParameter.From(items));

            var output = new StringBuilder();
            output.AppendLine("OMGHAI!");
            for (var i = 0; i <= numberOfDays; i++)
            {
                output.AppendLine($"-------- day {i} --------");
                output.AppendLine("name, sellIn, quality");
                foreach (var item in items)
                {
                    output.AppendLine($"{item.Name}, {item.SellIn}, {item.Quality}");
                }
                output.AppendLine();
                app.PerformEndOfDayUpdates();
            }

            Console.Write(output.ToString());
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<GildedRoseModule>();
            return builder.Build();
        }
    }
}
