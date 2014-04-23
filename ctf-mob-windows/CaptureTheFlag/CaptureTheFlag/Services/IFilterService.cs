using Caliburn.Micro;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public interface IFilterService
    {
        Task<BindableCollection<T>> FilterCollectionAsync<T>(BindableCollection<T> sourceCollection, Expression<Func<string>> property, Regex regex);
    }
}
