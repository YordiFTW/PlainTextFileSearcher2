using System;
using System.Collections.Generic;
using System.Text;

namespace PlainTextFileSearcher.Business
{
    class SearchResult
    {
        public int Id { get; set; }

        public string Result { get; set; }

        public string FilePath { get; set; }

        public string SeachTerm { get; set; }
    }
}
