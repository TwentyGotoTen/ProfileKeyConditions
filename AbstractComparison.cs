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
    public abstract class AbstractComparison<T> : OperatorCondition<T> where T : RuleContext
    {
        public string ProfileKeyId1 { get; set; }
        public string ProfileKeyId2 { get; set; }

        protected double ProfileKeyValue1
        {
            get
            {
                Assert.ArgumentNotNull((object)ProfileKeyId1, "ProfileKeyId1");
                return GetProfileKeyValue(ProfileKeyId1);
            }
        }

        protected double ProfileKeyValue2
        {
            get
            {
                Assert.ArgumentNotNull((object)ProfileKeyId2, "ProfileKeyId2");
                return GetProfileKeyValue(ProfileKeyId2);
            }
        }

        protected abstract double GetAdjustedComparisonValue(double rawVal);

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull((object)ruleContext, "ruleContext");
            double comparisonVal1 = GetProfileKeyValue(ProfileKeyId1);
            double profileKeyVal2 = GetProfileKeyValue(ProfileKeyId2);
            double comparisonVal2 = GetAdjustedComparisonValue(profileKeyVal2);
            return ValuesCompare(comparisonVal1, comparisonVal2, GetOperator());
        }

        private bool ValuesCompare(double val1, double val2,ConditionOperator op)
        {
            Sitecore.Diagnostics.Log.Info(val1.ToString() + " >= " + val2.ToString() + (val1 >= val2 ? ": true" : " false"),this);

            switch (op)
            {
                case ConditionOperator.Equal:
                    return val1 == val2;
                case ConditionOperator.GreaterThanOrEqual:
                    return val1 >= val2;
                case ConditionOperator.GreaterThan:
                    return val1 > val2;
                case ConditionOperator.LessThanOrEqual:
                    return ProfileKeyValue1 <= val2;
                case ConditionOperator.LessThan:
                    return val1 < val2;
                case ConditionOperator.NotEqual:
                    return val1 != val2;
                default:
                    return false;
            }          
        }

        private double GetProfileKeyValue(string profileKeyId)
        {
            if (string.IsNullOrEmpty(profileKeyId))
                return 0.0;

            Item profileKeyItem = Tracker.DefinitionDatabase.GetItem(profileKeyId);

            if (profileKeyItem == null)
                return 0.0;

            Item profileItem = profileKeyItem.Parent;

            if (profileItem == null)
                return 0.0;
            
            Tracker.Visitor.Load(new VisitorLoadOptions()
            {
                Start = Tracker.Visitor.GetOrCreateCurrentVisit().VisitorVisitIndex,
                Count = 1,
                VisitLoadOptions = VisitLoadOptions.Profiles,
                Options = VisitorOptions.None
            });

            String profileItemName = profileItem.Name.ToLower();
            IEnumerable<VisitorDataSet.ProfilesRow> allProfiles = Tracker.CurrentVisit.Profiles;           
            VisitorDataSet.ProfilesRow row = allProfiles.FirstOrDefault(r => r.ProfileName.ToLower() == profileItemName);
            
            if (row == null)
                return 0.0;
            
            return (double)row.GetValue(profileKeyItem.Name);
        }
    }
}
