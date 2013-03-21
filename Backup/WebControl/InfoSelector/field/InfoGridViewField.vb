<Serializable()> _
Public Class InfoGridViewField

    Private _colName As String = String.Empty
    Public Property colName() As String
        Get
            If String.IsNullOrEmpty(_colName) Then
                Throw New ArgumentException("if you declare <jw:Field /> tag, you must specify ""colName"" attribute")
            End If
            Return _colName
        End Get
        Set(ByVal value As String)
            _colName = value
        End Set
    End Property

    Private _Type As InfoGridview.eFieldType = InfoGridview.eFieldType.Label

    Public Property Type() As InfoGridview.eFieldType
        Get
            Return _Type
        End Get
        Set(ByVal value As InfoGridview.eFieldType)
            _Type = value
        End Set
    End Property

    Private _Title As String = String.Empty
    Public Property Title() As String
        Get
            If String.IsNullOrEmpty(_Title) Then
                _Title = colName
            End If
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
End Class