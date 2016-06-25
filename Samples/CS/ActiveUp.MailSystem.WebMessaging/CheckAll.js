function checkall()
{
	if(event.srcElement.checked)
	{
	    for(var i=0; i < document.forms[0].elements.length; i++) 
	    document.forms[0].elements[i].checked = true;
	}
	else
	{ 
	    for(var i=0; i < document.forms[0].elements.length; i++)
	    document.forms[0].elements[i].checked = false;
	}
}