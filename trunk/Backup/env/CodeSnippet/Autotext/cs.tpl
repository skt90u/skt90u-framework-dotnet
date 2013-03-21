a::#d:
#define 
readme:
property (get + set)

a:property (get + set):jproperty:
#region $summary$
/// <summary>
/// $summary$
/// </summary>
private $type$ $property$
{
	get
	{
		return $property$;
	}
	set
	{
		$property$ = value;
	}
}
private $type$ $property$;
#endregion


readme:
jDependencyProperty

a:jDependencyProperty:jDependencyProperty:
public static readonly DependencyProperty $name$Property = DependencyProperty.Register("$name$", typeof($propertyType$), , typeof($ownerType$), null);
public $propertyType$ $name$
{
	get
	{
		return ($propertyType$) this.GetValue( $name$Property );
	}
	set
	{
	this.SetValue( $name$Property , value );
	}
}
readme:
end

a:end:end:
// end of $class$
readme:
include current project's core namespace

a:include current project's core namespace:jusing:
using IHLCD.Controls;
using IHLCD.Kernel;
using IHLCD.Kernel.HelperTrinity;
readme:
region block

a:region block:jreg:
#region $region$
$end$
#endregion
readme:
Argument Validation

a:Argument Validation:jargv:
//
// The most appropriate validation method in following statements
//

// for any type
$arg$.AssertNotNull("$arg$");

// for IEnumerable
$arg$.AssertNotNull("$arg$", true /* assertContentsNotNull */);

// for string
$arg$.AssertNotNullOrEmpty("$arg$");
$arg$.AssertNotNullOrEmpty("$arg$", true /* trim */);
readme:
lazy initialization

a:lazy initialization:jlazyinit:
#region $summary$
/// <summary>
/// $summary$
/// </summary>
public $type$ $var$
{
	get
	{
		if ($var$ == null)
		{
			$var$ = $end$;
		}
		return $var$;
	}
}
private $type$ $var$;
#endregion
readme:
comment block

a:comment block:jcomment:
/*
 $end$
 */
readme:
try catch block with Log.ReportError(ex)

a:try catch block with Log.ReportError(ex):jtry:
try
{
$end$
}
catch (Exception ex)
{
	Log.ReportError(ex);
}
a::#if:
#if $end$
$selected$
#endif

a::b:
bool
a::r:
return
a::n:
null
a::fl:
float
a::n0:
!= 0
a::Gui:
GuidAttribute("$GUID_STRING$"), 
a:namespace { ... }:namespace:
namespace $end$
{
	$selected$
}

a::struct:
struct $end$ 
{
}
a::switch:
switch ($end$)
{
	$selected$
}

a::switch:
switch ($end$)
{
case :
	break;
}

a:://-:
// $end$ [$MONTH$/$DAY$/$YEAR$ %USERNAME%]
a::///:
//////////////////////////////////////////////////////////////////////////

a:<summary> ... </summary>:sum:
/// <summary>
///   $end$
/// </summary>

a::/*-:
/*
 *	$end$
 */
a::/**:
/************************************************************************/
/* $end$                                                                     */
/************************************************************************/
a:if () { ... }:if:
if ($end$)
{
	$selected$
}

a:if () { ... } else { }:if:
if ($end$)
{
	$selected$
} 
else
{
}

a:if () { } else { ... }::
if ($end$)
{
} 
else
{
	$selected$
}

a:while () { ... }:while:
while ($end$)
{
	$selected$
}

a:for () { ... }:for:
for ($end$)
{
	$selected$
}

a:for loop forward:forr:
for (int $Index$ = 0; $Index$ < $Length$ ; $Index$++)
{
	$end$
}

a:for loop reverse:forr:
for (int $Index$ = $Length$ - 1; $Index$ >= 0 ; $Index$--)
{
	$end$
}

a::fore:
foreach ($end$)
{
}

a:do { ... } while ():do:
do 
{
	$selected$
} while ($end$);

a:try { ... } catch {}:try:
try
{
	$selected$
}
catch (System.Exception ex)
{
	$end$
}

a:try { ... } catch {} finally {}:try:
try
{
	$selected$
}
catch (System.Exception ex)
{
	$end$
}
finally
{
}

a::bas:
base.$MethodName$($MethodArgs$);


a:File header detailed::
/********************************************************************
	created:	$DATE$
	created:	$DAY$:$MONTH$:$YEAR$   $HOUR$:$MINUTE$
	filename: 	$FILE$
	file path:	$FILE_PATH$
	file base:	$FILE_BASE$
	file ext:	$FILE_EXT$
	author:		$Author$
	
	purpose:	$end$
*********************************************************************/

readme:
VA Snippet used for suggestions in loops.
Delete this item to restore the default upon next use.

a:SuggestionsForType loop::
continue;
break;

readme:
VA Snippet used for suggestions in switch statements.
Delete this item to restore the default upon next use.

a:SuggestionsForType switch::
case
default:
break;

readme:
VA Snippet used for suggestions in class definitions.
Delete this item to restore the default upon next use.

a:SuggestionsForType class::
public
private
protected
virtual
void
bool
string
static
override
internal

readme:
VA Snippet used for suggestions of type bool.
Delete this item to restore the default upon next use.

a:SuggestionsForType bool::
true
false

readme:
VA Snippet used for suggestions of type Boolean.
Delete this item to restore the default upon next use.

a:SuggestionsForType Boolean::
true
false

readme:
VA Snippet used for refactoring: Change Signature, Create Implementation, and Move Implementation to Source File.
Delete this item to restore the default upon next use.

a:Refactor Create Implementation::
$SymbolPrivileges$ $SymbolType$ $SymbolName$( $ParameterList$ )
{
	$end$$MethodBody$
}

readme:
VA Snippet used for refactoring.
Delete this item to restore the default upon next use.

a:Refactor Document Method::
/// <summary>
/// $end$
/// </summary>
/// <param name="$MethodArgName$"></param>
/// <returns></returns>

readme:
VA Snippet used for refactoring.
Delete this item to restore the default upon next use.

a:Refactor Encapsulate Field::
	public $SymbolType$ $end$$GeneratedPropertyName$
	{
		get { return $SymbolName$; }
		set { $SymbolName$ = value; }
	}

readme:
VA Snippet used for refactoring.
Delete this item to restore the default upon next use.

a:Refactor Extract Method::

$end$$SymbolPrivileges$ $SymbolType$ $SymbolContext$( $ParameterList$ )
{
	$MethodBody$
}

readme:
VA Snippet used for refactoring: Create From Usage and Implement Interface.
Delete this item to restore the default upon next use.

a:Refactor Create From Usage Method Body::
throw new Exception("The method or operation is not implemented.");
readme:
VA Snippet used by Surround With #region.
Delete this item to restore the default upon next use.

a:#region (VA X):#r:
#region $end$
$selected$
#endregion

readme:
Delete this item to restore the default when the IDE starts.

a:{...}::
{
	$end$$selected$
}

readme:
Delete this item to restore the default when the IDE starts.

a:(...)::
($selected$)
readme:
extend existed class

a:extend existed class:jext:
public static class Ext$type$
{
	public static void aExtFunc(this $type$ self)
	{
		
	}


} // end of Ext$type$
