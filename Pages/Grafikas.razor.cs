using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Charts;
using Blazorise.Charts.Streaming;
using Microsoft.AspNetCore.Components;

namespace MedzMokslas.Pages
{
    public partial class Grafikas : ComponentBase
    {
        private LineChart<LiveDataPoint> horizontalLineChart;

        private Random random = new Random(DateTime.Now.Millisecond);

        private string[] Labels = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
        private List<string> backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 0.5f), ChartColor.FromRgba(54, 162, 235, 0.5f), ChartColor.FromRgba(255, 206, 86, 0.5f), ChartColor.FromRgba(75, 192, 192, 0.5f), ChartColor.FromRgba(153, 102, 255, 0.5f), ChartColor.FromRgba(255, 159, 64, 0.5f) };
        private List<string> borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        public struct LiveDataPoint 
        {
            public object X { get; set; }
            public object Y { get; set; }
        }

        private object horizontalLineChartOptions = new
        {
            Title = new
            {
                Display = true,
                Text = "Grafikas"
            },
            Scales = new
            {
                YAxes = new object[]
                {
                    new
                    {
                        ScaleLabel = new
                        {
                            Display = true, LabelString = "Varža"
                        }
                    }
                }
            },
            Tooltips = new
            {
                Mode = "nearest",
                Intersect = false
            },
            Hover = new
            {
                Mode = "nearest",
                Intersect = false
            }
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) 
            {
                await Task.WhenAll(
                    HandleRedraw(horizontalLineChart, GetLineChartDataset1, GetLineChartDataset2, GetLineChartDataset3));
            }
        }

        private async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(BaseChart<TDataSet, TItem, TOptions, TModel> chart, params Func<TDataSet>[] getDataSets)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.Clear();

            await chart.AddLabelsDatasetsAndUpdate(Labels, getDataSets.Select(x => x.Invoke()).ToArray());
        }

        private LineChartDataset<LiveDataPoint> GetLineChartDataset1()
        {
            return new()
            {
                Data = new(),
                Label = "Pimras metalas",
                BackgroundColor = backgroundColors[0],
                BorderColor = borderColors[0],
                Fill = false,
                CubicInterpolationMode = "monotone",
            };
        }

        private LineChartDataset<LiveDataPoint> GetLineChartDataset2()
        {
            return new()
            {
                Data = new(),
                Label = "Antras metalas",
                BackgroundColor = backgroundColors[1],
                BorderColor = borderColors[1],
                Fill = false,
                CubicInterpolationMode = "monotone",
            };        
        }

        private LineChartDataset<LiveDataPoint> GetLineChartDataset3()
        {
            return new()
            {
                Data = new(),
                Label = "Trecias metalas",
                BackgroundColor = backgroundColors[2],
                BorderColor = borderColors[2],
                Fill = false,
                CubicInterpolationMode = "monotone",
            };
        }

        private Task OnHorizontalLineRefreshed1(ChartStreamingData<LiveDataPoint> data) 
        {
            data.Value = new()
            {
                X = DateTime.Now,
                Y = ivestaTemp / 2,
            };


            return Task.CompletedTask;
        }

        private Task OnHorizontalLineRefreshed2(ChartStreamingData<LiveDataPoint> data)
        {
            data.Value = new()
            {
                X = DateTime.Now,
                Y = ivestaTemp,
            };

            return Task.CompletedTask;
        }

        private Task OnHorizontalLineRefreshed3(ChartStreamingData<LiveDataPoint> data)
        {
            data.Value = new()
            {
                X = DateTime.Now,
                Y = ivestaTemp * 2,
            };

            return Task.CompletedTask;
        }
    }
}
