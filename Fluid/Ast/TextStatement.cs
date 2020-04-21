﻿using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Fluid.Ast
{
    public class TextStatement : Statement
    {
        private readonly char[] _buffer;

        public TextStatement(StringSegment text)
        {
            _buffer = new char[text.Length];
            text.Buffer.CopyTo(text.Offset, _buffer, 0, text.Length);
        }

        public TextStatement(string text)
        {
            _buffer = text.ToCharArray();
        }

        public string Text => new String(_buffer);

        public override ValueTask<Completion> WriteToAsync(TextWriter writer, TextEncoder encoder, TemplateContext context)
        {
            context.IncrementSteps();

            // The Text fragments are not encoded, but kept as-is
            writer.Write(_buffer);

            return Normal;
        }
    }
}
