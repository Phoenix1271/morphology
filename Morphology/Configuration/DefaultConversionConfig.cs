using System.Collections.Generic;
using Morphology.Conversion;
using Morphology.Conversion.Policies;

namespace Morphology.Configuration
{
    internal class DefaultConversionConfig : IConversionConfig
    {
        #region Constructors

        public DefaultConversionConfig(ILogger logger)
        {
            ByteArrayLimit = 1024;
            ConversionLimit = 10;
            ItemLimit = 1000;
            StringLimit = 0;
            Policies = new IConversionPolicy[]
            {
                new StringConversionPolicy(this),
                new ScalarConversionPolicy(),
                new EnumConversionPolicy(),
                new ByteArrayConversionPolicy(this),
                new DelegateConversionPolicy(),
                new ReflectionTypeConversionPolicy(),
                new DictionaryConversionPolicy(this),
                new CollectionConversionPolicy(this),
                new StructureConversionPolicy(logger)
            };
        }

        #endregion

        #region IConversionConfig

        public int ByteArrayLimit { get; }
        public int ConversionLimit { get; }
        public int ItemLimit { get; }

        public IEnumerable<IConversionPolicy> Policies { get; }

        public int StringLimit { get; }

        #endregion
    }
}
