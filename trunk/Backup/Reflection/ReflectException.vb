' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org.
' ****************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' Summary description for NoTestMethodsException.
''' </summary>
''' 
<Serializable()> _
Public Class InvalidTestFixtureException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
    End Sub

    ''' <summary>
    ''' Serialization Constructor
    ''' </summary>
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class

' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org.
' ****************************************************************

''' <summary>
''' Thrown when an assertion failed. Here to preserve the inner
''' exception and hence its stack trace.
''' </summary>
''' 
<Serializable()> _
Public Class NUnitException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Standard constructor
    ''' </summary>
    ''' <param name="message">The error message that explains 
    ''' the reason for the exception</param>
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Standard constructor
    ''' </summary>
    ''' <param name="message">The error message that explains 
    ''' the reason for the exception</param>
    ''' <param name="inner">The exception that caused the 
    ''' current exception</param>
    Public Sub New(ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
    End Sub

    ''' <summary>
    ''' Serialization Constructor
    ''' </summary>
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub


End Class
