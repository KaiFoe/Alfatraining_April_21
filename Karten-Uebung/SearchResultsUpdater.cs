using System;
using UIKit;

namespace Karten_Uebung
{
    public class SearchResultsUpdater : UISearchResultsUpdating
    {

        public event Action<string> UpdateSearchResults = delegate { };

        public SearchResultsUpdater()
        {
        }

        public override void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            UpdateSearchResults(searchController.SearchBar.Text);
        }
    }
}
