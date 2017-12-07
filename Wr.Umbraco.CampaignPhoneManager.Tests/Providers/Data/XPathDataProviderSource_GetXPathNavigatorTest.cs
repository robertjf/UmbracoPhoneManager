﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers
{
    class XPathDataProviderSource_GetXPathNavigatorTest : IXPathDataProviderSource
    {
        private string _testPhoneManagerData;
        public XPathDataProviderSource_GetXPathNavigatorTest(string testPhoneManagerData)
        {
            _testPhoneManagerData = testPhoneManagerData.Replace("<?xml version=\"1.0\"?>", "");
        }

        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            var textReader = new StringReader(DataHelpers.GetContentXml(_testPhoneManagerData));
            var navigatorResult = new XPathDocument(textReader).CreateNavigator()
                                .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            CampaignPhoneManagerModel result = XmlHelper.XPathNavigatorToModel<CampaignPhoneManagerModel>(navigatorResult);

            return (result != null) ? result : new CampaignPhoneManagerModel();
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            var textReader = new StringReader(DataHelpers.GetContentXml(_testPhoneManagerData));
            var navigatorResult = new XPathDocument(textReader).CreateNavigator()
                                .Select(xpath).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }
    }
}