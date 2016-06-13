function CheckForm()
{
	if(event.srcElement.name=='composer:iSubmit')
	{
		var fromOk = CheckFrom();
		var toOk = CheckTo();
		if(fromOk && toOk)
		{
			__doPostBack(event.srcElement.name,'');
			event.srcElement.disabled = true;
		}
		else return false;
	}
}
function CheckFrom()
{
	if(event.srcElement.form.elements.namedItem('composer:iFromEmail').value==null || event.srcElement.form.elements.namedItem('composer:iFromEmail').value.length<3)
	{
		event.srcElement.form.elements.namedItem('composer:iFromEmail').style.backgroundColor='red';
		return false;
	}
	else return true;
}
function CheckTo()
{
	if(event.srcElement.form.elements.namedItem('composer:iTo').value==null || event.srcElement.form.elements.namedItem('composer:iTo').value.length<3)
	{
		event.srcElement.form.elements.namedItem('composer:iTo').style.backgroundColor='red';
		return false;
	}
	else return true;
}