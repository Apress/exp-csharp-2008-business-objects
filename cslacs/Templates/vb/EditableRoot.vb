<Serializable()> _
Public Class EditableRoot
  Inherits BusinessBase(Of EditableRoot)

#Region " Business Methods "

  ' TODO: add your own fields, properties and methods

  'example with private backing field
  Private Shared IdProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Id", "Id"))
  Private _id As Integer = IdProperty.DefaultValue
  ''' <Summary>
  ''' Gets and sets the Id value.
  ''' </Summary>
  Public Property Id() As Integer
    Get
      Return GetProperty(IdProperty, _id)
    End Get
    Set(ByVal value As Integer)
      SetProperty(IdProperty, _id, value)
    End Set
  End Property

  'example with managed backing field
  Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name", "Name"))
  ''' <Summary>
  ''' Gets and sets the Name value.
  ''' </Summary>
  Public Property Name() As String
    Get
      Return GetProperty(NameProperty)
    End Get
    Set(ByVal value As String)
      SetProperty(NameProperty, value)
    End Set
  End Property

#End Region

#Region " Validation Rules "

  Protected Overrides Sub AddBusinessRules()

    ' TODO: add validation rules
    'ValidationRules.AddRule(Nothing, "")

  End Sub

#End Region

#Region " Authorization Rules "

  Protected Overrides Sub AddAuthorizationRules()

    ' TODO: add authorization rules
    'AuthorizationRules.AllowWrite(NameProperty, "")

  End Sub

  Private Shared Sub AddObjectAuthorizationRules()
    'TODO: add authorization rules
    'AuthorizationRules.AllowEdit(GetType(EditableRoot), "Role")
  End Sub

#End Region

#Region " Factory Methods "

  Public Shared Function NewEditableRoot() As EditableRoot
    Return DataPortal.Create(Of EditableRoot)()
  End Function

  Public Shared Function GetEditableRoot(ByVal id As Integer) As EditableRoot
    Return DataPortal.Fetch(Of EditableRoot)(New SingleCriteria(Of EditableRoot, Integer)(id))
  End Function

  Public Shared Sub DeleteEditableRoot(ByVal id As Integer)
    DataPortal.Delete(New SingleCriteria(Of EditableRoot, Integer)(id))
  End Sub

  Private Sub New()
    ' require use of factory methods
  End Sub

#End Region

#Region " Data Access "

  <RunLocal()> _
  Protected Overrides Sub DataPortal_Create()
    ' TODO: load default values
    ' omit this override if you have no defaults to set
    MyBase.DataPortal_Create()
  End Sub

  Private Overloads Sub DataPortal_Fetch(ByVal criteria As SingleCriteria(Of EditableRoot, Integer))
    ' load values
  End Sub

  <Transactional(TransactionalTypes.TransactionScope)> _
  Protected Overrides Sub DataPortal_Insert()
    ' TODO: insert values
  End Sub

  <Transactional(TransactionalTypes.TransactionScope)> _
  Protected Overrides Sub DataPortal_Update()
    ' TODO: update values
  End Sub

  <Transactional(TransactionalTypes.TransactionScope)> _
  Protected Overrides Sub DataPortal_DeleteSelf()
    DataPortal_Delete(New SingleCriteria(Of EditableRoot, Integer)(Id))
  End Sub

  Private Overloads Sub DataPortal_Delete(ByVal criteria As SingleCriteria(Of EditableRoot, Integer))
    ' TODO: delete values
  End Sub

#End Region

End Class
