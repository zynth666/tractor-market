﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TractorMarket.Helpers;

public sealed class DeepObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
{
    public DeepObservableCollection()
    {
        CollectionChanged += FullObservableCollectionCollectionChanged;
    }

    public DeepObservableCollection(IEnumerable<T> pItems) : this()
    {
        foreach (var item in pItems)
        {
            this.Add(item);
        }
    }

    private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (object item in e.NewItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
            }
        }
        if (e.OldItems != null)
        {
            foreach (object item in e.OldItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
            }
        }
    }

    private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
        OnCollectionChanged(args);
    }
}
