
using System.Web.Mvc;

namespace AddressBook.Utilities
{
    public static class Utilities
    {
    //  The function is used to set the current Nav-Item with the 'active' class 
    //  by returning either "active" or blank
        public static string TabActive(this HtmlHelper html, string TabID)
        {
            var routeTab = html.ViewBag.TabID;  //  check what TabID is in viewbag
            routeTab = string.IsNullOrEmpty(routeTab) ? "A" : routeTab;     //defaults to Tab'A'

            int substrIndex = (routeTab.Length)-1;
            //route_substrIndex = routetab_length - 1

            routeTab = routeTab.Substring(substrIndex);   //removes preamble from routedata action
            routeTab = routeTab.ToUpper();

            var active = TabID == routeTab; //True if TabID matches TabID in URL

            return active ? "active" : "";  //apply active class to the element calling this function
        }
    }
}