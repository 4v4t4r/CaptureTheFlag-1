using Caliburn.Micro;
using CaptureTheFlag.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;

namespace CaptureTheFlag.Services
{
    public class FilterService : IFilterService //TODO: Refactor, check for errors, nulls in property
    {
        public CancellationTokenSource cancelSource;
        public Task<BindableCollection<T>> FilterCollectionAsync<T>(BindableCollection<T> sourceCollection, Expression<Func<string>> property, Regex regex)
        {
            if (cancelSource != null)
            {
                cancelSource.Cancel();
            }
            cancelSource = new CancellationTokenSource();
            CancellationToken cancelSourceToken = cancelSource.Token;
            BindableCollection<T> filteredCollection = new BindableCollection<T>();

            return Task<BindableCollection<T>>.Run(() =>
                {
                    if ((sourceCollection != null) && (sourceCollection.Count > 0) && (property != null))
                    {
                        PropertyInfo itemProperty = getProperty(sourceCollection[0], property);
                        if (itemProperty != null)
                        {
                            Debug.WriteLine("{0}", itemProperty.GetValue(sourceCollection.First()));

                            Debug.WriteLine("Task {0}: has to filter {1} elements", Task.CurrentId, sourceCollection.Count);
                            int i = 1;
                            foreach (T item in sourceCollection)
                            {
                                if (cancelSourceToken.IsCancellationRequested)
                                {
                                    Debug.WriteLine("Task {0}, requested cancel!", Task.CurrentId);
                                    return sourceCollection; //NOTE: Failed to catch OperationCanceledException
                                }
                                if (itemProperty.GetValue(item) != null && regex.IsMatch((string)itemProperty.GetValue(item)))
                                {

                                    filteredCollection.Add(item);
                                }
                                if ((i % 100) == 0)
                                {
                                    Debug.WriteLine("Task {0}: Loop count {1}", Task.CurrentId, i);
                                }
                                ++i;
                            }
                        }
                    }
                    Debug.WriteLine("Task {0}: filtered emelents to {1}", Task.CurrentId, filteredCollection.Count);
                    return filteredCollection;
                }, cancelSourceToken);
        }

        private PropertyInfo getProperty<T>(T obj, Expression<Func<string>> property)
        {
            MemberInfo member = property.GetMemberInfo();
            string name = member.Name;
            PropertyInfo itemProperty = obj.GetType().GetProperty(name);
            return itemProperty;
        }

    }
   
}
