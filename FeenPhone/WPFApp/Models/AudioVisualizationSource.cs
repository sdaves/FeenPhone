﻿using FeenPhone.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeenPhone.WPFApp.Models
{
    public class AudioVisualizationSource : DependencyObject
    {
        public static DependencyProperty LevelDbProperty = DependencyProperty.Register("LevelDb", typeof(double), typeof(AudioVisualizationSource));
        public static DependencyProperty LevelDbPercentProperty = DependencyProperty.Register("LevelDbPercent", typeof(double), typeof(AudioVisualizationSource));
        private Audio.SampleAggregator aggregator;

        public AudioVisualizationSource(Audio.SampleAggregator aggregator)
        {
            this.aggregator = aggregator;

            MaximumCalculated += new EventHandler<MaxSampleEventArgs>(audioGraph_MaximumCalculated);
            FftCalculated += new EventHandler<FftEventArgs>(audioGraph_FftCalculated);
        }

        event EventHandler<FeenPhone.Audio.FftEventArgs> FftCalculated
        {
            add { aggregator.FftCalculated += value; }
            remove { aggregator.FftCalculated -= value; }
        }

        event EventHandler<FeenPhone.Audio.MaxSampleEventArgs> MaximumCalculated
        {
            add { aggregator.MaximumCalculated += value; }
            remove { aggregator.MaximumCalculated -= value; }
        }

        void audioGraph_FftCalculated(object sender, FftEventArgs e)
        {
            //Dispatcher.BeginInvoke(new Action<object, FftEventArgs>((s, args) =>
            //{
            //    //if (this.selectedVisualization != null)
            //    //{
            //    //    this.selectedVisualization.OnFftCalculated(e.Result);
            //    //}
            //    //spectrumAnalyser.Update(e.Result);
            //}), sender, e);
        }

        double MinDb { get { return -60; } }
        double MaxDb { get { return 0; } }

        void audioGraph_MaximumCalculated(object sender, MaxSampleEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<object, MaxSampleEventArgs>((s, args) =>
            {
                double db = 20 * Math.Log10(args.MaxSample);
                if (db < MinDb)
                    db = MinDb;
                if (db > MaxDb)
                    db = MaxDb;
                double percent = ((db - MinDb) / (MaxDb - MinDb)) * 100;

                SetValue(LevelDbProperty, db);
                SetValue(LevelDbPercentProperty, percent);
            }), sender, e);
        }


    }
}
