'LE-BE conversions
#If Config <= Nightly Then 'Stage:Nightly
Partial Class Math
    ''' <summary>Converts <see cref="Short"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    Public Overloads Shared Function LEBE(ByVal value As Short) As Short
        Dim B1 As Byte = (value And &HFF00) >> 8
        Dim B2 As Byte = value And &HFF
        Return CShort(B2) << 8 Or CShort(B1)
    End Function
    ''' <summary>Converts <see cref="UShort"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    <CLSCompliant(False)> _
    Public Overloads Shared Function LEBE(ByVal value As UShort) As UShort
        Dim B1 As Byte = (value And &HFF00) >> 8
        Dim B2 As Byte = value And &HFF
        Return CUShort(B2) << 8 Or CUShort(B1)
    End Function
    ''' <summary>Converts <see cref="Integer"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    Public Overloads Shared Function LEBE(ByVal value As Integer) As Integer
        Dim B1 As Byte = (value And &HFF000000) >> (3 * 8)
        Dim B2 As Byte = (value And &HFF0000) >> (2 * 8)
        Dim B3 As Byte = (value And &HFF00) >> 8
        Dim B4 As Byte = (value And &HFF)
        Return CInt(B4) << (3 * 8) Or CInt(B3) << (2 * 8) Or CInt(B2) << 8 Or B1
    End Function
    ''' <summary>Converts <see cref="UInteger"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    <CLSCompliant(False)> _
    Public Overloads Shared Function LEBE(ByVal value As UInteger) As UInteger
        Dim B1 As Byte = (value And &HFF000000) >> (3 * 8)
        Dim B2 As Byte = (value And &HFF0000) >> (2 * 8)
        Dim B3 As Byte = (value And &HFF00) >> 8
        Dim B4 As Byte = (value And &HFF)
        Return CUInt(B4) << (3 * 8) Or CUInt(B3) << (2 * 8) Or CUInt(B2) << 8 Or B1
    End Function
    ''' <summary>Converts <see cref="Long"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    Public Overloads Shared Function LEBE(ByVal value As Long) As Long
        Dim B1 As Byte = (value And &HFF00000000000000) >> (7 * 8)
        Dim B2 As Byte = (value And &HFF000000000000) >> (6 * 8)
        Dim B3 As Byte = (value And &HFF0000000000) >> (5 * 8)
        Dim B4 As Byte = (value And &HFF00000000) >> (4 * 8)
        Dim B5 As Byte = (value And &HFF000000) >> (3 * 8)
        Dim B6 As Byte = (value And &HFF0000) >> (2 * 8)
        Dim B7 As Byte = (value And &HFF00) >> 8
        Dim B8 As Byte = (value And &HFF)
        Return CLng(B8) << (7 * 8) Or CLng(B7) << (6 * 8) Or CLng(B6) << (5 * 8) Or CLng(B5) << (4 * 8) Or CLng(B4) << (3 * 8) Or CLng(B3) << (2 * 8) Or CLng(B2) << 8 Or CLng(B1)
    End Function
    ''' <summary>Converts <see cref="ULong"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    <Author("�onny", "dzony@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(Math), LastChange:="04/23/2007")> _
    <CLSCompliant(False)> _
    Public Overloads Shared Function LEBE(ByVal value As ULong) As ULong
        Dim B1 As Byte = (value And &HFF00000000000000) >> (7 * 8)
        Dim B2 As Byte = (value And &HFF000000000000) >> (6 * 8)
        Dim B3 As Byte = (value And &HFF0000000000) >> (5 * 8)
        Dim B4 As Byte = (value And &HFF00000000) >> (4 * 8)
        Dim B5 As Byte = (value And &HFF000000) >> (3 * 8)
        Dim B6 As Byte = (value And &HFF0000) >> (2 * 8)
        Dim B7 As Byte = (value And &HFF00) >> 8
        Dim B8 As Byte = (value And &HFF)
        Return CULng(B8) << (7 * 8) Or CULng(B7) << (6 * 8) Or CULng(B6) << (5 * 8) Or CULng(B5) << (4 * 8) Or CULng(B4) << (3 * 8) Or CULng(B3) << (2 * 8) Or CULng(B2) << 8 Or CULng(B1)
    End Function
End Class
#End If