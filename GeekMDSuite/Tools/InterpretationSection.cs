﻿using System.Collections.Generic;

namespace GeekMDSuite.Tools
{
    public class InterpretationSection 
    {
        public string Title { get; }
        public List<string> Paragraphs { get; }

        public InterpretationSection(string title, List<string> paragraphs)
        {
            Title = title;
            Paragraphs = paragraphs;
        }
    }
}