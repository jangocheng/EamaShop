using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public interface ISearchEnginee<TListModel, TConditions>
       where TListModel : ISearchableMetadata
        where TConditions : ISearchableCondition
    {
        Task<(int Total,IEnumerable<TListModel>)> SearchAsync(TConditions conditions);
    }
}
