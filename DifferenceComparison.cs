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
        public String Adjustment { get; set; }

        protected override double GetAdjustedComparisonValue(double rawVal)
        {
            if (String.IsNullOrEmpty(Adjustment))
                return rawVal;
            
            double adjustmentAsDouble = 0;
            Double.TryParse(Adjustment, out adjustmentAsDouble);

            return rawVal + adjustmentAsDouble; 
        } 
    }
}
