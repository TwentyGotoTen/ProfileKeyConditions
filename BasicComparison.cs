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
    public class BasicComparison<T> : AbstractComparison<T> where T : RuleContext
    {
        protected override bool Comparison
        {
            get
            {
                switch (this.GetOperator())
                {
                    case ConditionOperator.Equal:
                        return ProfileKeyValue1 == ProfileKeyValue2;
                    case ConditionOperator.GreaterThanOrEqual:
                        return ProfileKeyValue1 >= ProfileKeyValue2;
                    case ConditionOperator.GreaterThan:
                        return ProfileKeyValue1 > ProfileKeyValue2;
                    case ConditionOperator.LessThanOrEqual:
                        return ProfileKeyValue1 <= ProfileKeyValue2;
                    case ConditionOperator.LessThan:
                        return ProfileKeyValue1 < ProfileKeyValue2;
                    case ConditionOperator.NotEqual:
                        return ProfileKeyValue1 != ProfileKeyValue2;
                    default:
                        return false;
                }
            }
        }
    }
}
