' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org.
' ****************************************************************

Imports System.Reflection
Imports System.Collections

''' <summary>
''' Helper methods for inspecting a type by reflection. 
''' 
''' Many of these methods take ICustomAttributeProvider as an 
''' argument to avoid duplication, even though certain attributes can 
''' only appear on specific types of members, like MethodInfo or Type.
''' 
''' In the case where a type is being examined for the presence of
''' an attribute, interface or named member, the Reflect methods
''' operate with the full name of the member being sought. This
''' removes the necessity of the caller having a reference to the
''' assembly that defines the item being sought and allows the
''' NUnit core to inspect assemblies that reference an older
''' version of the NUnit framework.
''' </summary>
Public Class Reflect
    Private Shared ReadOnly AllMembers As BindingFlags = BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy

    Public Shared Function GetCallingType() As Type
        Dim type As Type = Nothing

        Dim stackTrace As New StackTrace()
        Try
            type = stackTrace.GetFrame(2).GetMethod().DeclaringType
        Catch ex As Exception
        Finally
            stackTrace = Nothing
        End Try
        Return type
    End Function

#Region "Attributes"

    ''' <summary>
    ''' Check presence of attribute of a given type on a member.
    ''' </summary>
    ''' <param name="member">The member to examine</param>
    ''' <param name="attrName">The FullName of the attribute type to look for</param>
    ''' <param name="inherit">True to include inherited attributes</param>
    ''' <returns>True if the attribute is present</returns>
    Public Shared Function HasAttribute(ByVal member As ICustomAttributeProvider, ByVal attrName As String, ByVal inherit As Boolean) As Boolean
        For Each attribute As Attribute In GetAttributes(member, inherit)
            If IsInstanceOfType(attrName, attribute) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Get attribute of a given type on a member. If multiple attributes
    ''' of a type are present, the first one found is returned.
    ''' </summary>
    ''' <param name="member">The member to examine</param>
    ''' <param name="attrName">The FullName of the attribute type to look for</param>
    ''' <param name="inherit">True to include inherited attributes</param>
    ''' <returns>The attribute or null</returns>
    Public Shared Function GetAttribute(ByVal member As ICustomAttributeProvider, ByVal attrName As String, ByVal inherit As Boolean) As System.Attribute
        For Each attribute As Attribute In GetAttributes(member, inherit)
            If IsInstanceOfType(attrName, attribute) Then
                Return attribute
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Get all attributes of a given type on a member.
    ''' </summary>
    ''' <param name="member">The member to examine</param>
    ''' <param name="attrName">The FullName of the attribute type to look for</param>
    ''' <param name="inherit">True to include inherited attributes</param>
    ''' <returns>The attribute or null</returns>
    Public Shared Function GetAttributes(ByVal member As ICustomAttributeProvider, ByVal attrName As String, ByVal inherit As Boolean) As System.Attribute()
        Dim result As New ArrayList()
        For Each attribute As Attribute In GetAttributes(member, inherit)
            If IsInstanceOfType(attrName, attribute) Then
                result.Add(attribute)
            End If
        Next
        Return DirectCast(result.ToArray(GetType(System.Attribute)), System.Attribute())
    End Function

    ''' <summary>
    ''' Get all attributes on a member.
    ''' </summary>
    ''' <param name="member">The member to examine</param>
    ''' <param name="inherit">True to include inherited attributes</param>
    ''' <returns>The attribute or null</returns>
    Public Shared Function GetAttributes(ByVal member As ICustomAttributeProvider, ByVal inherit As Boolean) As System.Attribute()
        Dim attributes As Object() = member.GetCustomAttributes(inherit)
        Dim result As System.Attribute() = New System.Attribute(attributes.Length - 1) {}
        Dim n As Integer = 0
        For Each attribute As Attribute In attributes
            result(System.Math.Max(System.Threading.Interlocked.Increment(n), n - 1)) = attribute
        Next

        Return result
    End Function

#End Region

#Region "Interfaces"

    ''' <summary>
    ''' Check to see if a type implements a named interface.
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="interfaceName">The FullName of the interface to check for</param>
    ''' <returns>True if the interface is implemented by the type</returns>
    Public Shared Function HasInterface(ByVal fixtureType As Type, ByVal interfaceName As String) As Boolean
        For Each type As Type In fixtureType.GetInterfaces()
            If type.FullName = interfaceName Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "Inheritance"

    ''' <summary>
    ''' Checks to see if a type inherits from a named type. 
    ''' </summary>
    ''' <param name="type">The type to examine</param>
    ''' <param name="typeName">The FullName of the inherited type to look for</param>
    ''' <returns>True if the type inherits from the named type.</returns>
    ''' <remarks></remarks>
    Public Shared Function InheritsFrom(ByVal type As Type, ByVal typeName As String) As Boolean
        Dim current As Type = type
        While current IsNot GetType(Object)
            If current.FullName = typeName Then
                Return True
            End If
            current = current.BaseType
        End While

        Return False
    End Function

    Public Shared Function InheritsFrom(ByVal obj As Object, ByVal typeName As String) As Boolean
        Return InheritsFrom(obj.[GetType](), typeName)
    End Function

    Public Shared Function IsInstanceOfType(ByVal typeName As String, ByVal attr As Attribute) As Boolean
        Dim type As Type = attr.[GetType]()
        Return type.FullName = typeName OrElse InheritsFrom(type, typeName)
    End Function
#End Region

#Region "Get Methods of a type"

    ''' <summary>
    ''' Find the default constructor on a type
    ''' </summary>
    ''' <param name="fixtureType"></param>
    ''' <returns></returns>
    Public Shared Function GetConstructor(ByVal fixtureType As Type) As ConstructorInfo
        Return fixtureType.GetConstructor(Type.EmptyTypes)
    End Function

    ''' <summary>
    ''' Examine a fixture type and return an array of methods having a 
    ''' particular attribute. The array is order with base methods first.
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="attributeName">The FullName of the attribute to look for</param>
    ''' <returns>The array of methods found</returns>
    Public Shared Function GetMethodsWithAttribute(ByVal fixtureType As Type, ByVal attributeName As String, ByVal inherit As Boolean) As MethodInfo()
        Dim list As New ArrayList()

        For Each method As MethodInfo In GetMethods(fixtureType)
            If HasAttribute(method, attributeName, inherit) Then
                list.Add(method)
            End If
        Next

        list.Sort(New BaseTypesFirstComparer())

        Return DirectCast(list.ToArray(GetType(MethodInfo)), MethodInfo())
    End Function

    Private Shared Function GetMethods(ByVal fixtureType As Type) As MethodInfo()
        Dim result As MethodInfo() = fixtureType.GetMethods(AllMembers)

        Return result
    End Function

    Private Class BaseTypesFirstComparer
        Implements IComparer
#Region "IComparer Members"

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim m1 As MethodInfo = TryCast(x, MethodInfo)
            Dim m2 As MethodInfo = TryCast(y, MethodInfo)

            If m1 Is Nothing OrElse m2 Is Nothing Then
                Return 0
            End If

            Dim m1Type As Type = m1.DeclaringType
            Dim m2Type As Type = m2.DeclaringType

            If m1Type Is m2Type Then
                Return 0
            End If
            If m1Type.IsAssignableFrom(m2Type) Then
                Return -1
            End If

            Return 1
        End Function

#End Region
    End Class

    ''' <summary>
    ''' Examine a fixture type and return true if it has a method with
    ''' a particular attribute. 
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="attributeName">The FullName of the attribute to look for</param>
    ''' <returns>True if found, otherwise false</returns>
    Public Shared Function HasMethodWithAttribute(ByVal fixtureType As Type, ByVal attributeName As String, ByVal inherit As Boolean) As Boolean
        For Each method As MethodInfo In GetMethods(fixtureType)
            If HasAttribute(method, attributeName, inherit) Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Examine a fixture type and get a method with a particular name.
    ''' In the case of overloads, the first one found is returned.
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="methodName">The name of the method</param>
    ''' <returns>A MethodInfo or null</returns>
    Public Shared Function GetNamedMethod(ByVal fixtureType As Type, ByVal methodName As String) As MethodInfo
        For Each method As MethodInfo In GetMethods(fixtureType)
            If method.Name = methodName Then
                Return method
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Examine a fixture type and get a method with a particular name and list
    ''' of arguments. In the case of overloads, the first one found is returned.
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="methodName">The name of the method</param>
    ''' <param name="argTypes">The full names of the argument types to search for</param>
    ''' <returns>A MethodInfo or null</returns>
    Public Shared Function GetNamedMethod(ByVal fixtureType As Type, ByVal methodName As String, ByVal argTypes As String()) As MethodInfo
        For Each method As MethodInfo In GetMethods(fixtureType)
            If method.Name = methodName Then
                Dim parameters As ParameterInfo() = method.GetParameters()
                If parameters.Length = argTypes.Length Then
                    Dim match As Boolean = True
                    For i As Integer = 0 To argTypes.Length - 1
                        If parameters(i).ParameterType.FullName <> argTypes(i) Then
                            match = False
                            Exit For
                        End If
                    Next

                    If match Then
                        Return method
                    End If
                End If
            End If
        Next

        Return Nothing
    End Function

#End Region

#Region "Get Properties of a type"
    ''' <summary>
    ''' Examine a type and return a property having a particular attribute.
    ''' In the case of multiple methods, the first one found is returned.
    ''' </summary>
    ''' <param name="fixtureType">The type to examine</param>
    ''' <param name="attributeName">The FullName of the attribute to look for</param>
    ''' <returns>A PropertyInfo or null</returns>
    Public Shared Function GetPropertyWithAttribute(ByVal fixtureType As Type, ByVal attributeName As String) As PropertyInfo
        For Each [property] As PropertyInfo In fixtureType.GetProperties(AllMembers)
            If HasAttribute([property], attributeName, True) Then
                Return [property]
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Examine a type and get a property with a particular name.
    ''' In the case of overloads, the first one found is returned.
    ''' </summary>
    ''' <param name="type">The type to examine</param>
    ''' <param name="bindingFlags">BindingFlags to use</param>
    ''' <returns>A PropertyInfo or null</returns>
    Public Shared Function GetNamedProperty(ByVal type As Type, ByVal name As String, ByVal bindingFlags As BindingFlags) As PropertyInfo
        Return type.GetProperty(name, bindingFlags)
    End Function

    ''' <summary>
    ''' Get the value of a named property on an object using binding flags of Public and Instance
    ''' </summary>
    ''' <param name="obj">The object for which the property value is needed</param>
    ''' <param name="name">The name of a non-indexed property of the object</param>
    ''' <returns></returns>
    Public Shared Function GetPropertyValue(ByVal obj As Object, ByVal name As String) As Object
        Return GetPropertyValue(obj, name, BindingFlags.[Public] Or BindingFlags.Instance)
    End Function

    ''' <summary>
    ''' Get the value of a named property on an object
    ''' </summary>
    ''' <param name="obj">The object for which the property value is needed</param>
    ''' <param name="name">The name of a non-indexed property of the object</param>
    ''' <param name="bindingFlags">BindingFlags for use in determining which properties are needed</param>param>
    ''' <returns></returns>
    Public Shared Function GetPropertyValue(ByVal obj As Object, ByVal name As String, ByVal bindingFlags As BindingFlags) As Object
        Dim [property] As PropertyInfo = GetNamedProperty(obj.[GetType](), name, bindingFlags)
        If [property] IsNot Nothing Then
            Return [property].GetValue(obj, Nothing)
        End If
        Return Nothing
    End Function
#End Region

#Region "Get Fields of a type"
    Public Shared Function GetFieldValue(ByVal obj As Object, ByVal name As String) As Object
        Return GetFieldValue(obj, name, BindingFlags.[Public] Or BindingFlags.Instance)
    End Function

    Public Shared Function GetFieldValue(ByVal obj As Object, ByVal name As String, ByVal bindingFlags As BindingFlags) As Object
        Dim [fieldInfo] As FieldInfo = GetNamedField(obj.[GetType](), name, bindingFlags)
        If [fieldInfo] IsNot Nothing Then
            Return [fieldInfo].GetValue(obj)
        End If
        Return Nothing
    End Function

    Public Shared Function GetNamedField(ByVal type As Type, ByVal name As String, ByVal bindingFlags As BindingFlags) As FieldInfo
        Return type.GetField(name, bindingFlags)
    End Function
#End Region

#Region "Invoke Methods"

    ''' <summary>
    ''' Invoke the default constructor on a Type
    ''' </summary>
    ''' <param name="type">The Type to be constructed</param>
    ''' <returns>An instance of the Type</returns>
    Public Shared Function Construct(ByVal type As Type) As Object
        Dim ctor As ConstructorInfo = GetConstructor(type)
        If ctor Is Nothing Then
            Throw New InvalidTestFixtureException(type.FullName & " does not have a default constructor")
        End If

        Return ctor.Invoke(Nothing)
    End Function

    ''' <summary>
    ''' Invoke a constructor on a Type with arguments
    ''' </summary>
    ''' <param name="type">The Type to be constructed</param>
    ''' <param name="arguments">Arguments to the constructor</param>
    ''' <returns>An instance of the Type</returns>
    Public Shared Function Construct(ByVal type As Type, ByVal arguments As Object()) As Object
        If arguments Is Nothing Then
            Return Construct(type)
        End If

        'Type[] argTypes = GetTypeArray(arguments);
        'ConstructorInfo ctor = GetConstructor(type, argTypes);
        'if (ctor == null)
        '    throw new InvalidTestFixtureException(type.FullName + " does not have a suitable constructor");

        Try
            Return type.InvokeMember(type.Name, BindingFlags.CreateInstance, Nothing, Nothing, arguments)
        Catch ex As Exception
            Throw New InvalidTestFixtureException(type.FullName & " does not have a suitable constructor", ex)
        End Try
    End Function

    ''' <summary>
    ''' Returns an array of types from an array of objects.
    ''' Used because the compact framework doesn't support
    ''' Type.GetTypeArray()
    ''' </summary>
    ''' <param name="objects">An array of objects</param>
    ''' <returns>An array of Types</returns>
    Public Shared Function GetTypeArray(ByVal objects As Object()) As Type()
        Dim types As Type() = New Type(objects.Length - 1) {}
        Dim index As Integer = 0
        For Each o As Object In objects
            types(System.Math.Max(System.Threading.Interlocked.Increment(index), index - 1)) = If(o Is Nothing, Nothing, o.[GetType]())
        Next
        Return types
    End Function

    ''' <summary>
    ''' Invoke a parameterless method returning void on an object.
    ''' </summary>
    ''' <param name="method">A MethodInfo for the method to be invoked</param>
    ''' <param name="fixture">The object on which to invoke the method</param>
    Public Shared Function InvokeMethod(ByVal method As MethodInfo, ByVal fixture As Object) As Object
        Return InvokeMethod(method, fixture, Nothing)
    End Function

    ''' <summary>
    ''' Invoke a method returning void, converting any TargetInvocationException
    ''' to an NUnitException
    ''' </summary>
    ''' <param name="method">A MethodInfo for the method to be invoked</param>
    ''' <param name="fixture">The object on which to invoke the method</param>
    Public Shared Function InvokeMethod(ByVal method As MethodInfo, ByVal fixture As Object, ByVal ParamArray args As Object()) As Object
        If method IsNot Nothing Then
            Try
                Return method.Invoke(fixture, args)
            Catch e As TargetInvocationException
                Dim inner As Exception = e.InnerException
                Throw New NUnitException("Rethrown", inner)
            End Try
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' (2011.01.08 JELLY) 擴充InvokeMethod 
    ''' </summary>
    Public Shared Function InvokeMethod(ByVal methodName As String, ByVal fixture As Object, ByVal ParamArray args As Object()) As Object
        Dim t As Type = fixture.GetType()
        Dim mi As MethodInfo = t.GetMethod(methodName, BindingFlags.NonPublic Or BindingFlags.Instance)
        Return InvokeMethod(mi, fixture, args)
    End Function
#End Region

#Region "Private Constructor for static-only class"

    Private Sub New()
    End Sub

#End Region
End Class
