using System;
using Csla;

namespace Templates
{
  [Serializable]
  public class EditableRootParent : BusinessBase<EditableRootParent>
  {
    #region Business Methods

    // TODO: add your own fields, properties and methods

    // example with private backing field
    private static PropertyInfo<int> IdProperty =
      RegisterProperty(new PropertyInfo<int>("Id"));
    private int _Id = IdProperty.DefaultValue;
    public int Id
    {
      get { return GetProperty(IdProperty, _Id); }
      set { SetProperty(IdProperty, ref _Id, value); }
    }

    // example with managed backing field
    private static PropertyInfo<string> NameProperty =
      RegisterProperty(new PropertyInfo<string>("Name"));
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    private static PropertyInfo<EditableChildList> ChildListProperty = 
      RegisterProperty<EditableChildList>(new PropertyInfo<EditableChildList>("ChildList", "Child list"));
    public EditableChildList ChildList
    {
      get { return GetProperty<EditableChildList>(ChildListProperty); }
    }

    private static PropertyInfo<EditableChild> ChildProperty = 
      RegisterProperty(new PropertyInfo<EditableChild>("Child", "Child"));
    public EditableChild Child
    {
      get { return GetProperty<EditableChild>(ChildProperty); }
    }
    #endregion

    #region Validation Rules

    protected override void AddBusinessRules()
    {
      // TODO: add validation rules
      //ValidationRules.AddRule(RuleMethod, "");
    }

    #endregion

    #region Authorization Rules

    protected override void AddAuthorizationRules()
    {
      // TODO: add authorization rules
      //AuthorizationRules.AllowWrite("Name", "Role");
    }

    private static void AddObjectAuthorizationRules()
    {
      // TODO: add authorization rules
      //AuthorizationRules.AllowEdit(typeof(EditableRootParent), "Role");
    }

    #endregion

    #region Factory Methods

    public static EditableRootParent NewEditableRootParent()
    {
      return DataPortal.Create<EditableRootParent>();
    }

    public static EditableRootParent GetEditableRootParent(int id)
    {
      return DataPortal.Fetch<EditableRootParent>(
        new SingleCriteria<EditableRootParent, int>(id));
    }

    public static void DeleteEditableRootParent(int id)
    {
      DataPortal.Delete(new SingleCriteria<EditableRootParent, int>(id));
    }

    private EditableRootParent()
    { /* Require use of factory methods */ }

    #endregion

    #region Data Access

    [RunLocal]
    protected override void DataPortal_Create()
    {
      // TODO: load default values
      // omit this override if you have no defaults to set
      LoadProperty(ChildListProperty, EditableChildList.NewEditableChildList());
      LoadProperty(ChildProperty, EditableChild.NewEditableChild());
      base.DataPortal_Create();
    }

    private void DataPortal_Fetch(SingleCriteria<EditableRootParent, int> criteria)
    {
      // TODO: load values
      LoadProperty(ChildListProperty, EditableChildList.GetEditableChildList(null));
      LoadProperty(ChildProperty, EditableChild.GetEditableChild(null));
    }

    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_Insert()
    {
      // TODO: insert values
      FieldManager.UpdateChildren(this);
    }

    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_Update()
    {
      // TODO: update values
      FieldManager.UpdateChildren(this);
    }

    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_DeleteSelf()
    {
      DataPortal_Delete(new SingleCriteria<EditableRootParent, int>(this.Id));
    }

    [Transactional(TransactionalTypes.TransactionScope)]
    private void DataPortal_Delete(SingleCriteria<EditableRootParent, int> criteria)
    {
      // TODO: delete values
      FieldManager.UpdateChildren(this);
    }

    #endregion
  }
}
