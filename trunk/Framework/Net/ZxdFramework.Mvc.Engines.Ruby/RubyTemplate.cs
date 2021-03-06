﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using IronRubyMvc;

namespace ZxdFramework.Mvc.Engines.Ruby {
    public class RubyTemplate {
        List<string> _requires = new List<string>();
        string _template;

        public RubyTemplate(string templateContents) {
            if (templateContents == null)
                throw new ArgumentNullException("templateContents");

            _template = templateContents;
        }

        public void AddRequire(string require) {
            _requires.Add(require);
        }

        public string ToScript() {
            return ToScript(null);
        }

        public string ToScript(string methodName) {
            StringBuilder builder = new StringBuilder();
            ToScript(methodName, builder);
            return builder.ToString().Trim();
        }

        public void ToScript(string methodName, StringBuilder builder) {
            string contents = _template;

            builder.AppendLine();
            _requires.ForEach(require => builder.AppendLine(string.Format("require '{0}'", require)));

            if (!String.IsNullOrEmpty(methodName))
                builder.AppendLine("def " + methodName);

            Regex scriptBlocks = new Regex("<%.*?%>", RegexOptions.Compiled | RegexOptions.Singleline);
            MatchCollection matches = scriptBlocks.Matches(contents);

            int currentIndex = 0;
            int blockBeginIndex = 0;

            foreach (Match match in matches) {
                blockBeginIndex = match.Index;
                RubyScriptBlock block = RubyScriptBlock.Parse(contents.Substring(currentIndex, blockBeginIndex - currentIndex));

                if (!String.IsNullOrEmpty(block.Contents))
                    builder.AppendLine(block.Contents);

                block = RubyScriptBlock.Parse(match.Value);
                builder.AppendLine(block.Contents);
                currentIndex = match.Index + match.Length;
            }

            if (currentIndex < contents.Length - 1) {
                RubyScriptBlock endBlock = RubyScriptBlock.Parse(contents.Substring(currentIndex));
                builder.Append(endBlock.Contents);
            }

            if (!String.IsNullOrEmpty(methodName)) {
                builder.AppendLine();
                builder.AppendLine("end");
            }
        }
    }
}