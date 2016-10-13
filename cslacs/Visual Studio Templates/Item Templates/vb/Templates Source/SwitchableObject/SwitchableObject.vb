Imports System
Imports Csla
Imports Csla.Security

namespace $rootnamespace$

  <Serializable()> _
  Public Class $safeitemname$
    Inherits BusinessBase(Of $safeitemname$)

#Region "Business Methods"

    ' TODO: add your own fields, properties and methods 

    ' example with private backing field 
    Private Shared IdProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Id"))
    Private _Id As Integer = IdProperty.DefaultValue
    Public Property Id() As Integer
      Get
        Return GetProperty(IdProperty, _Id)
      End Get
      Set(ByVal value As Integer)
        SetProperty(IdProperty, _Id, value)
      End Set
    End Property

    ' example with managed backing field 
    Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
    Public Property Name() As String
      Get
        Return GetProperty(NameProperty)
      End Get
      Set(ByVal value As String)
        SetProperty(NameProperty, value)
      End Set
    End Property

#End Region

#Region "Validation Rules"

    Protected Overloads Overrides Sub AddBusinessRules()
      ' TODO: add validation rules 
      ' ValidationRules.AddRule(Nothing, "")
    End Sub

#End Region

#Region "Authorization Rules"

    Protected Overloads Overrides Sub AddAuthorizationRules()
      ' TODO: add authorization rules 
      AuthorizationRules.AllowWrite(NameProperty, "Role")
    End Sub

    Private Shared Sub AddObjectAuthorizationRules()
      ' TODO: add authorization rules 
      ' AuthorizationRules.AllowEdit(GetType($safeitemname$), "Role")
    End Sub

#End Region

#Region "Root Factory Methods"

    Public Shared Function New$safeitemname$() As $safeitemname$
      Return DataPortal.Create(Of $safeitemname$)()
    End Function

    Public Shared Function Get$safeitemname$(ByVal id As Integer) As $safeitemname$
      Return DataPortal.Fetch(Of $safeitemname$)(New SingleCriteria(Of $safeitemname$, Integer)(id))
    End Function

    Public Shared Sub Delete$safeitemname$(ByVal id As Integer)
      DataPortal.Delete(New SingleCriteria(Of $safeitemname$, Integer)(id))
    End Sub

#End Region

#Region "Child Factory Methods"

    Friend Shared Function New$safeitemname$() As $safeitemname$
      Return DataPortal.CreateChild(Of $safeitemname$)()
    End Function

    Friend Shared Function Get$safeitemname$(ByVal childData As Object) As $safeitemname$
      Return DataPortal.FetchChild(Of $safeitemname$)(childData)
    End Function

    ' Require use of factory methods 
    Private Sub New()
    End Sub

#End Region

#Region "Root Data Access"

    <RunLocal()> _
    Protected Overloads Overrides Sub DataPortal_Create()
      ' TODO: load default values 
      ' omit this override if you have no defaults to set 
      MyBase.DataPortal_Create()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As SingleCriteria(Of $safeitemname$, Integer))
      ' TODO: load values 
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> _
    Protected Overloads Overrides Sub DataPortal_Insert()
      ' TODO: insert values 
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> _
    Protected Overloads Overrides Sub DataPortal_Update()
      ' TODO: update values 
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> _
    Protected Overloads Overrides Sub DataPortal_DeleteSelf()
      DataPortal_Delete(New SingleCriteria(Of $safeitemname$, Integer)(Me.Id))
    End Sub

    <Transactional(TransactionalTypes.TransactionScope)> _
    Private Overloads Sub DataPortal_Delete(ByVal criteria As SingleCriteria(Of $safeitemname$, Integer))
      ' TODO: delete values 
    End Sub

#End Region

#Region "Child Data Access"

    Protected Overloads Overrides Sub Child_Create()
      ' TODO: load default values 
      ' omit this override if you have no defaults to set 
      MyBase.Child_Create()
    End Sub

    Private Sub Child_Fetch(ByVal childData As Object)
      ' TODO: load values 
    End Sub

    Private Sub Child_Insert(ByVal parent As Object)
      ' TODO: insert values 
    End Sub

    Private Sub Child_Update(ByVal parent As Object)
      ' TODO: update values 
    End Sub

    Private Sub Child_DeleteSelf(ByVal parent As Object)
      ' TODO: delete values 
    End Sub

#End Region
  End Class

End Namespace