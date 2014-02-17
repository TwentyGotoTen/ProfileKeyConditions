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

        protected abstract bool Comparison { get; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull((object)ruleContext, "ruleContext");

            double profileKeyValue1 = this.GetProfileKeyValue(ProfileKeyId1);
            double profileKeyValue2 = this.GetProfileKeyValue(ProfileKeyId2);

            return Comparison;
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
