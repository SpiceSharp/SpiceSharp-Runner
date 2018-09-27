﻿using System;
using SpiceSharpParser;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Evaluation;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Readers.Controls.Plots;

namespace SpiceSharpRunner.Windows.Logic
{
    public class SpiceHelper
    {
        public static SpiceSharpParser.ModelReaders.Netlist.Spice.SpiceNetlistReaderResult GetSpiceSharpNetlist(string netlist, SpiceEvaluatorMode evaluatorMode, int? randomSeed)
        {
            var parser = new SpiceParser();
            parser.Settings.Parsing.HasTitle = true;
            parser.Settings.Parsing.IsNewlineRequired = true;
            parser.Settings.Parsing.IsEndRequired = false;
            parser.Settings.Reading.Seed = randomSeed;
            parser.Settings.Reading.EvaluatorMode = evaluatorMode;
            parser.Settings.Reading.EvaluatorMode = SpiceEvaluatorMode.Spice3f5;
            parser.Settings.WorkingDirectory = Environment.CurrentDirectory;

            var parserResult = parser.ParseNetlist(netlist);
            return parserResult.Result;
        }

        public static bool IsPlotPositive(XyPlot plot)
        {
            for (var i = 0; i < plot.Series.Count; i++)
            {
                for (var j = 0; j < plot.Series[i].Points.Count; j++)
                {
                    var y = plot.Series[i].Points[j].Y;
                    if (y <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
 