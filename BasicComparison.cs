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
        protected override double GetAdjustedComparisonValue(double rawVal)
        {
            return rawVal;
        } 
    }
}
