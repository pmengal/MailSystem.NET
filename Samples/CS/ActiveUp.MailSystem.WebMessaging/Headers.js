function ShowHeaders()
{
	if(document.getElementById('headersDisplay').innerHTML=='')
	{
		document.getElementById('headersDisplay').innerHTML=document.getElementById('headers').innerHTML;
		document.getElementById('more').src = 'icons/tree-open-h.gif';
	}
	else
	{
		document.getElementById('headersDisplay').innerHTML='';
		document.getElementById('more').src = 'icons/tree-close-h.gif';
	}
}