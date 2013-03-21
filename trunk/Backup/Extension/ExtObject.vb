Imports System.Runtime.CompilerServices
Imports System.ComponentModel

''' <summary>Enhance Object functionality</summary>
Public Module ExtObject

    ''' <summary>determines whether the specified value is of numeric type.</summary>
    <Extension()> _
    Public Function IsNumericType(ByVal value As Object) As Boolean
        Return (TypeOf value Is Byte OrElse _
                TypeOf value Is SByte OrElse _
                TypeOf value Is Short OrElse _
                TypeOf value Is UShort OrElse _
                TypeOf value Is Integer OrElse _
                TypeOf value Is UInteger OrElse _
                TypeOf value Is Long OrElse _
                TypeOf value Is ULong OrElse _
                TypeOf value Is Single OrElse _
                TypeOf value Is Double OrElse _
                TypeOf value Is Decimal)
    End Function

#Region "Type Casting"

    ''' <summary>implement a casting operation that support nullable object type-casting</summary>
    <Extension()> _
    Public Function ConvertTo(ByVal value As Object, ByVal conversionType As Type) As Object
        'http://aspalliance.com/852
        If conversionType.IsGenericType AndAlso conversionType.GetGenericTypeDefinition().Equals(GetType(Nullable(Of ))) Then
            Dim nullableConverter As New NullableConverter(conversionType)
            conversionType = nullableConverter.UnderlyingType
        End If
        Return Convert.ChangeType(value, conversionType)
    End Function

    ''' <summary>determines whether the specified value is positive.</summary>
    <Extension()> _
    Public Function IsPositive(ByVal value As Object, ByVal zeroIsPositive As Boolean) As Boolean
        Select Case Type.GetTypeCode(value.[GetType]())
            Case TypeCode.[SByte]
                Return (If(zeroIsPositive, CSByte(value) >= 0, CSByte(value) > 0))
            Case TypeCode.Int16
                Return (If(zeroIsPositive, CShort(value) >= 0, CShort(value) > 0))
            Case TypeCode.Int32
                Return (If(zeroIsPositive, CInt(value) >= 0, CInt(value) > 0))
            Case TypeCode.Int64
                Return (If(zeroIsPositive, CLng(value) >= 0, CLng(value) > 0))
            Case TypeCode.[Single]
                Return (If(zeroIsPositive, CSng(value) >= 0, CSng(value) > 0))
            Case TypeCode.[Double]
                Return (If(zeroIsPositive, CDbl(value) >= 0, CDbl(value) > 0))
            Case TypeCode.[Decimal]
                Return (If(zeroIsPositive, CDec(value) >= 0, CDec(value) > 0))
            Case TypeCode.[Byte]
                Return (If(zeroIsPositive, True, CByte(value) > 0))
            Case TypeCode.UInt16
                Return (If(zeroIsPositive, True, CUShort(value) > 0))
            Case TypeCode.UInt32
                Return (If(zeroIsPositive, True, CUInt(value) > 0))
            Case TypeCode.UInt64
                Return (If(zeroIsPositive, True, CULng(value) > 0))
            Case TypeCode.[Char]
                Return (If(zeroIsPositive, True, CChar(value) <> ControlChars.NullChar))
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>converts the specified values boxed type to its correpsonding unsigned type.</summary>
    <Extension()> _
    Public Function ToUnsigned(ByVal value As Object) As Object
        Select Case Type.GetTypeCode(value.[GetType]())
            Case TypeCode.[SByte]
                Return CByte(CSByte(value))
            Case TypeCode.Int16
                Return CUShort(CShort(value))
            Case TypeCode.Int32
                Return CUInt(CInt(value))
            Case TypeCode.Int64
                Return CULng(CLng(value))
            Case TypeCode.[Byte]
                Return value
            Case TypeCode.UInt16
                Return value
            Case TypeCode.UInt32
                Return value
            Case TypeCode.UInt64
                Return value
            Case TypeCode.[Single]
                Return CUInt(CSng(value))
            Case TypeCode.[Double]
                Return CULng(Math.Truncate(CDbl(value)))
            Case TypeCode.[Decimal]
                Return CULng(Math.Truncate(CDec(value)))
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>converts the specified values boxed type to its correpsonding integer type.</summary>
    <Extension()> _
    Public Function ToInteger(ByVal value As Object, ByVal round As Boolean) As Object
        Select Case Type.GetTypeCode(value.[GetType]())
            Case TypeCode.[SByte]
                Return value
            Case TypeCode.Int16
                Return value
            Case TypeCode.Int32
                Return value
            Case TypeCode.Int64
                Return value
            Case TypeCode.[Byte]
                Return value
            Case TypeCode.UInt16
                Return value
            Case TypeCode.UInt32
                Return value
            Case TypeCode.UInt64
                Return value
            Case TypeCode.[Single]
                Return (If(round, CInt(Math.Round(CSng(value))), CInt(Math.Truncate(CSng(value)))))
            Case TypeCode.[Double]
                Return (If(round, CLng(Math.Round(CDbl(value))), CLng(Math.Truncate(CDbl(value)))))
            Case TypeCode.[Decimal]
                Return (If(round, Math.Round(CDec(value)), CDec(value)))
            Case Else
                Return Nothing
        End Select
    End Function

    <Extension()> _
    Public Function UnboxToLong(ByVal value As Object, ByVal round As Boolean) As Long
        Select Case Type.GetTypeCode(value.[GetType]())
            Case TypeCode.[SByte]
                Return CLng(CSByte(value))
            Case TypeCode.Int16
                Return CLng(CShort(value))
            Case TypeCode.Int32
                Return CLng(CInt(value))
            Case TypeCode.Int64
                Return CLng(value)

            Case TypeCode.[Byte]
                Return CLng(CByte(value))
            Case TypeCode.UInt16
                Return CLng(CUShort(value))
            Case TypeCode.UInt32
                Return CLng(CUInt(value))
            Case TypeCode.UInt64
                Return CLng(CULng(value))

            Case TypeCode.[Single]
                Return (If(round, CLng(Math.Round(CSng(value))), CLng(Math.Truncate(CSng(value)))))
            Case TypeCode.[Double]
                Return (If(round, CLng(Math.Round(CDbl(value))), CLng(Math.Truncate(CDbl(value)))))
            Case TypeCode.[Decimal]
                Return (If(round, CLng(Math.Round(CDec(value))), CLng(Math.Truncate(CDec(value)))))
            Case Else

                Return 0
        End Select
    End Function
#End Region

End Module
