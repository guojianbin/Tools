﻿Imports System.Runtime.CompilerServices, System.Linq, System.Collections.Generic
#If True
Namespace LinqT
    ''' <summary>Tools for working with <see cref="IEnumerable(Of T)"/></summary>
    Public Module EnumerableT
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        ''' <remarks>Unification is done immediatelly.</remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        Public Function UnionAll(Of T)(ByVal ParamArray collections As IEnumerable(Of T)()) As IEnumerable(Of T)
            Return UnionAll(DirectCast(collections, IEnumerable(Of IEnumerable(Of T))))
        End Function
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an empty collection.</returns>
        ''' <remarks>Unification is done immediatelly causing all collections to be walked through on function call.</remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        <Extension()> Public Function UnionAll(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Dim ret As IEnumerable(Of T) = New List(Of T)
            If collections Is Nothing Then Return ret
            For Each item In collections
                ret = ret.Union(item)
            Next item
            Return ret
        End Function
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        ''' <remarks>This is alias of <see cref="UnionAll"/> which takes one parameter.
        ''' <para>Unification is done immediatelly.</para></remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        ''' <seelaso cref="UnionAll"/>
        <Extension()> Public Function FlatAll(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Return UnionAll(collections)
        End Function
        ''' <summary>Creates union of all geiven colections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collections</typeparam>
        ''' <returns><see cref="UnionEnumerable(Of T)"/> over <paramref name="collections"/></returns>
        ''' <remarks>Unioning is deffered to time when collections are iterated</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        <Extension()> Public Function FlatAllDeffered(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Return New UnionEnumerable(Of T)(collections)
        End Function
        ''' <summary>Unions all unique items in given collections to one collection</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all unique members of collections in <paramref name="collections"/></returns>
        <Extension()> Public Function FlatDistinct(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Dim ret As New List(Of T)
            If collections Is Nothing Then Return ret
            For Each collection In collections
                For Each item In collection
                    If Not ret.Contains(item) Then ret.Add(item)
                Next
            Next
            Return ret
        End Function
        ''' <summary>Unions all unique items in given collections to one collection using given <see cref="IComparer(Of T)"/></summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all unique members of collections in <paramref name="collections"/></returns>
        ''' <param name="comparer">Comparer used for distinguishing unique items</param>
        <Extension()> Public Function FlatDistinct(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T)), ByVal comparer As IEqualityComparer(Of T)) As IEnumerable(Of T)
            If comparer Is Nothing Then Throw New ArgumentNullException("comparer")
            Dim ret As New List(Of T)
            If collections Is Nothing Then Return ret
            For Each collection In collections
                For Each item In collection
                    If Not ret.Contains(item, comparer) Then ret.Add(item)
                Next
            Next
            Return ret
        End Function
        ''' <summary>Creates union of given collection with other given collections</summary>
        ''' <param name="collection">Firts collection for union</param>
        ''' <param name="OtherCollections">Other collections for union</param>
        ''' <typeparam name="T">Type of mmber in collection(s)</typeparam>
        ''' <returns>Collection that contains members of <paramref name="collection"/> as well as of all items in <paramref name="OtherCollections"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function UnionAll(Of T)(ByVal collection As IEnumerable(Of T), ByVal ParamArray OtherCollections As IEnumerable(Of T)()) As IEnumerable(Of T)
            Return UnionAll(collection, DirectCast(OtherCollections, IEnumerable(Of IEnumerable(Of T))))
        End Function
        ''' <summary>Creates union of given collection with other given collections</summary>
        ''' <param name="collection">Firts collection for union</param>
        ''' <param name="OtherCollections">Other collections for union</param>
        ''' <typeparam name="T">Type of mmber in collection(s)</typeparam>
        ''' <returns>Collection that contains members of <paramref name="collection"/> as well as of all items in <paramref name="OtherCollections"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function UnionAll(Of T)(ByVal collection As IEnumerable(Of T), ByVal OtherCollections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            Return collection.Union(UnionAll(OtherCollections))
        End Function
        ''' <summary>Gets value indicating if given collection is empty</summary>
        ''' <param name="collection">Collection to check emptyness of</param>
        ''' <returns>True if first element of collection cannot be enumerated using <paramref name="collection"/>.<see cref="System.Collections.Generic.IEnumerable.GetEnumerator">GetEnumerator</see>.<see cref="IEnumerator.MoveNext">MoveNext</see>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function IsEmpty(ByVal collection As IEnumerable) As Boolean
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            Dim enumerator = collection.GetEnumerator
            Return Not enumerator.MoveNext
        End Function
        ''' <summary>Gets value indicating if given collection is non-empty</summary>
        ''' <param name="collection">Collection to check non-emptyness of</param>
        ''' <returns>True if first element of collection can be enumerated using <paramref name="collection"/>.<see cref="System.Collections.Generic.IEnumerable.GetEnumerator">GetEnumerator</see>.<see cref="IEnumerator.MoveNext">MoveNext</see>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function Exists(ByVal collection As IEnumerable) As Boolean
            Return Not collection.IsEmpty
        End Function
        ''' <summary>Gets value indicating if collection contains exactly one element</summary>
        ''' <param name="collection">Collection to check</param>
        ''' <returns>True if collection contains exactly one element</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function [Single](ByVal collection As IEnumerable) As Boolean
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            Dim enumerator = collection.GetEnumerator
            Return enumerator.MoveNext AndAlso Not enumerator.MoveNext
        End Function

        ''' <summary>Gets index of first occurence of given item in given collection</summary>
        ''' <param name="collection">Collection to find item in</param>
        ''' <param name="item">Item to be found</param>
        ''' <returns>Index of first occurence of <paramref name="item"/> in <paramref name="collection"/> (compared using <see cref="System.Object.Equals"/>). -1 if <paramref name="item"/> is not found in <paramref name="collection"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function IndexOf(ByVal collection As IEnumerable, ByVal item As Object) As Integer
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            If TypeOf collection Is Array Then
                Return Array.IndexOf(collection, item)
            ElseIf TypeOf collection Is IList Then
                Return DirectCast(collection, IList).IndexOf(item)
            End If
            Dim i% = 0
            For Each iitem In collection
                If iitem Is Nothing AndAlso item Is Nothing OrElse (iitem IsNot Nothing AndAlso iitem.Equals(item)) Then Return i
                i += 1
            Next
            Return -1
        End Function
        ''' <summary>Gets index of first occurence of given item in given collection</summary>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <param name="collection">Collection to find item in</param>
        ''' <param name="item">Item to be found</param>
        ''' <returns>Index of first occurence of <paramref name="item"/> in <paramref name="collection"/> (compared using <see cref="System.Object.Equals"/>). -1 if <paramref name="item"/> is not found in <paramref name="collection"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function IndexOf(Of T)(ByVal collection As IEnumerable(Of T), ByVal item As T) As Integer
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            If TypeOf collection Is IList(Of T) Then
                Return DirectCast(collection, IList(Of T)).IndexOf(item)
            ElseIf TypeOf collection Is T() Then
                Return Array.IndexOf(Of T)(collection, item)
            End If
            Return IndexOf(DirectCast(collection, IEnumerable), DirectCast(item, Object))
        End Function

        ''' <summary>Gets index of first occurence of given item in given collection using <see cref="IEqualityComparer(Of T)"/></summary>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <param name="collection">Collection to find item in</param>
        ''' <param name="item">Item to be found</param>
        ''' <param name="comparer">An <see cref="IEqualityComparer(Of T)"/> to compare items</param>
        ''' <returns>Index of first occurence of <paramref name="item"/> in <paramref name="collection"/> (compared using <paramref name="comparer"/>). -1 if <paramref name="item"/> is not found in <paramref name="collection"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null or <paramref name="comparer"/> is null.</exception>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        <Extension()> Public Function IndexOf(Of T)(ByVal collection As IEnumerable(Of T), ByVal item As T, comparer As IEqualityComparer(Of T)) As Integer
            If comparer Is Nothing Then Throw New InvalidEnumArgumentException("comparer")
            Return collection.IndexOf(item, AddressOf comparer.Equals)
        End Function
        ''' <summary>Gets index of first occurence of given item in given collection using comparison method</summary>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <param name="collection">Collection to find item in</param>
        ''' <param name="item">Item to be found</param>
        ''' <returns>Index of first occurence of <paramref name="item"/> in <paramref name="collection"/> (compared using <paramref name="compare"/>). -1 if <paramref name="item"/> is not found in <paramref name="collection"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null or <paramref name="compare"/> is null.</exception>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        <Extension()> Public Function IndexOf(Of T)(ByVal collection As IEnumerable(Of T), ByVal item As T, compare As Func(Of T, T, Boolean)) As Integer
            If compare Is Nothing Then Throw New InvalidEnumArgumentException("compare")
            Return IndexOf(collection, Function(a) compare(a, item))
        End Function
        ''' <summary>Gets index of first occurence of item that matches predicate</summary>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <param name="collection">Collection to find item in</param>
        ''' <returns>Index of first itm in <paramref name="collection"/> for which <paramref name="compare"/> returns true. -1 if no such item was found</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null or <paramref name="compare"/> is null.</exception>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        <Extension()> Public Function IndexOf(Of T)(ByVal collection As IEnumerable(Of T), compare As Predicate(Of T)) As Integer
            If collection Is Nothing Then Throw New InvalidEnumArgumentException("collection")
            If compare Is Nothing Then Throw New InvalidEnumArgumentException("compare")
            Dim i As Integer = 0
            For Each i2 In collection
                If compare(i2) Then Return i
                i += 1
            Next
            Return -1
        End Function
    End Module
End Namespace
#End If

