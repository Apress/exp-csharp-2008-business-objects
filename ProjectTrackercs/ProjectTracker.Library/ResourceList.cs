using Csla;
using Csla.Data;
using System;
using System.Linq;

namespace ProjectTracker.Library
{
  [Serializable()]
  public class ResourceList : ReadOnlyListBase<ResourceList, ResourceInfo>
  {
    #region  Factory Methods

    public static ResourceList EmptyList()
    {
      return new ResourceList();
    }

    public static ResourceList GetResourceList()
    {
      return DataPortal.Fetch<ResourceList>();
    }

    private ResourceList()
    { /* require use of factory methods */ }

    #endregion

    #region  Data Access

    private void DataPortal_Fetch()
    {
      RaiseListChangedEvents = false;
      using (var ctx = 
        ContextManager<ProjectTracker.DalLinq.PTrackerDataContext>.
        GetManager(ProjectTracker.DalLinq.Database.PTracker))
      {
        var data = from r in ctx.DataContext.Resources
                   select new ResourceInfo(r.Id, r.LastName, r.FirstName);
        IsReadOnly = false;
        this.AddRange(data);
        IsReadOnly = true;
      }
      RaiseListChangedEvents = true;
    }

    #endregion

  }
}