﻿using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SpiceSharpParser.ModelReader.Netlist.Spice.Processors.Controls.Plots;

namespace SpiceSharpRunner.Windows.Logic
{
    /// <summary>
    /// Plot view model
    /// </summary>
    public class PlotViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlotViewModel"/> class.
        /// </summary>
        /// <param name="plot"></param>
        /// <param name="xLog"></param>
        /// <param name="yLog"></param>
        public PlotViewModel(Plot plot, bool xLog = false, bool yLog = false)
        {
            Model = CreateOxyPlotModel(plot, xLog, yLog);
        }

        /// <summary>
        /// Gets the plot model.
        /// </summary>
        public PlotModel Model { get; private set; }

        /// <summary>
        /// Creates Oxyplot library plot model
        /// </summary>
        /// <param name="plot">Plot data</param>
        /// <param name="xLog">Specifies whether x-axis is logaritmic</param>
        /// <param name="yLog">Specifies whether y-axis is logaritmic</param>
        /// <returns></returns>
        private static PlotModel CreateOxyPlotModel(Plot plot, bool xLog, bool yLog)
        {
            var tmp = new PlotModel { Title = plot.Name };

            for (var i = 0; i < plot.Series.Count; i++)
            {
                if (plot.Series[i].Points.Count > 1)
                {
                    var series = new LineSeries { Title = plot.Series[i].Name, MarkerType = MarkerType.None };
                    tmp.Series.Add(series);

                    for (var j = 0; j < plot.Series[i].Points.Count; j++)
                    {
                        var y = plot.Series[i].Points[j].Y;
                        series.Points.Add(new DataPoint(plot.Series[i].Points[j].X, y));
                    }
                }
                else
                {
                    var series = new ScatterSeries { Title = plot.Series[i].Name, MarkerType = MarkerType.Cross };
                    var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
                    scatterSeries.Points.Add(new ScatterPoint(plot.Series[i].Points[0].X, plot.Series[i].Points[0].Y));
                    tmp.Series.Add(scatterSeries);
                }
            }

            string xUnit = null;
            string yUnit = null;
            if (plot.Series.Count == 1)
            {
                xUnit = plot.Series[0].XUnit;
                yUnit = plot.Series[0].YUnit;
            }
            else
            {
                for (var i = 0; i < tmp.Series.Count; i++)
                {
                    tmp.Series[i].Title += plot.Series[i].YUnit;
                }
            }

            if (xLog)
            {
                tmp.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Bottom, Unit = xUnit });
            }
            else
            {
                tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Unit = xUnit });
            }

            if (yLog)
            {
                tmp.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Left, Unit = yUnit });
            }
            else
            {
                tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Unit = yUnit });
            }

            return tmp;
        }
    }
}
