using System.Collections.Generic;

using Autofac;

using GildedRose.Items;
using GildedRose.QualityCalculators;
using GildedRose.QualityCalculators.Implementations;
using GildedRoseKata;

namespace GildedRose.Autofac
{
    internal sealed class GildedRoseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<StandardQualityCalculator>().As<IQualityCalculator<Item>>().SingleInstance();
            builder.RegisterType<AppreciatingQualityCalculator>().As<IQualityCalculator<AppreciatingItem>>().SingleInstance();
            builder.RegisterType<VelbenQualityCalculator>().As<IQualityCalculator<VelbenItem>>().SingleInstance();
            builder.RegisterType<ConjuredQualityCalculator>().As<IQualityCalculator<ConjuredItem>>().SingleInstance();

            builder.Register((context, parameters) => ConstructGildedRose(context, parameters.TypedAs<IList<Item>>()) ).AsSelf();
        }

        private static GildedRoseKata.GildedRose ConstructGildedRose(IComponentContext context, IList<Item> items) =>
            new(context.Resolve<IQualityCalculator<Item>>(),
                context.Resolve<IQualityCalculator<AppreciatingItem>>(),
                context.Resolve<IQualityCalculator<VelbenItem>>(),
                context.Resolve<IQualityCalculator<ConjuredItem>>(),
                items);
    }
}
