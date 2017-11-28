﻿using System.Collections.Generic;
using System.Linq;

namespace GeekMDSuite.Interpretation
{
    public class Interpretation
    {
        public string Title { get; }
        public string Summary { get; }
        public List<InterpretationSection> Sections { get; }

        public Interpretation(string title, string summary, List<InterpretationSection> sections)
        {
            Title = title;
            Summary = summary;
            Sections = sections;
        }

        public override string ToString()
        {
            return $"{Title}\n\n{Summary}\n\n{BuildSections()}";
        }

        private string BuildSections()
        {
            return Sections.Aggregate(string.Empty, (current, section) => current + $"{section.Title}\n\n{BuildSectionParagraphs(section)}");
        }

        private static string BuildSectionParagraphs(InterpretationSection section)
        {
            return section.Paragraphs.Aggregate(string.Empty, (current, parapraph) => $"{current}{parapraph}\n\n");
        }
    }
}