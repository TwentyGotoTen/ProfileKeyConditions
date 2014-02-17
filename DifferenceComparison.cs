using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Data.DataAccess;
using Sitecore.Analytics.Data.DataAccess.DataSets;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileKeyConditions
{
    [UsedImplicitly]
    public class Difference<T> : AbstractComparison<T> where T : RuleContext
    {
        public String Value { get; set; }

        protected override bool Comparison
        {
            get
            {
                double val;

                if (!double.TryParse(Value, out val))
                    return false;

                double comparisonValue = ProfileKeyValue2 * val / 100;

                switch (this.GetOperator())
                {
                    case ConditionOperator.Equal:
                        return ProfileKeyValue1 == comparisonValue;
                    case ConditionOperator.GreaterThanOrEqual:
                        return ProfileKeyValue1 >= comparisonValue;
                    case ConditionOperator.GreaterThan:
                        return ProfileKeyValue1 > comparisonValue;
                    case ConditionOperator.LessThanOrEqual:
                        return ProfileKeyValue1 <= comparisonValue;
                    case ConditionOperator.LessThan:
                        return ProfileKeyValue1 < comparisonValue;
                    case ConditionOperator.NotEqual:
                        return ProfileKeyValue1 != comparisonValue;
                    default:
                        return false;
                }
            }
        }
    }
}
