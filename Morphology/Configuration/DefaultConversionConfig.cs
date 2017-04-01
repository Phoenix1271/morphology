using System.Collections.Generic;
using Morphology.Conversion;
using Morphology.Conversion.Policies;

namespace Morphology.Configuration
{
    internal class DefaultConversionConfig : IConversionConfig
    {
        #region IConversionConfig

        public int ByteArrayLimit { get; } = 1024;
        public int ConversionLimit { get; } = 10;
        public int ItemLimit { get; } = 1000;

        public IEnumerable<IConversionPolicy> Policies { get; } = new IConversionPolicy[]
        {
            new ScalarConversionPolicy(),
            new EnumConversionPolicy(),
            new ByteArrayConversionPolicy(),
            new DelegateConversionPolicy(),
            new ReflectionTypeConversionPolicy(),
            new DictionaryConversionPolicy(),
            new CollectionConversionPolicy(),
            new StructureConversionPolicy()
        };

        public int StringLimit { get; } = 0;

        #endregion
    }
}
