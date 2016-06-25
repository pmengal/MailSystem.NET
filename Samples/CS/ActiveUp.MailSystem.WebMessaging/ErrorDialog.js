var opacity = 0;
function ShowErrorDialog(Description,Message)
{
	document.getElementById("ErrorMessage").innerHTML = Description+" : <br /><br />"+Message+"<br /><br /><a style=\"text-decoration:none;color:darkblue;\" href=\"javascript:void(0)\" onclick=\"HideErrorDialog();\">Close</a>";
	document.getElementById("ErrorDialog").style.left = "200px";
	document.getElementById("ErrorDialog").style.top = "135px";
	DoFadeIn();
	//document.getElementById("ErrorDialog").style.filter = "Alpha(Opacity=50)";
}
function DoFadeIn()
{
	document.getElementById("ErrorDialog").style.filter = "Alpha(Opacity="+opacity+")";
	opacity += 10;
	if(opacity<90) setTimeout("DoFadeIn()",50);
}
function DoFadeOut()
{
	document.getElementById("ErrorDialog").style.filter = "Alpha(Opacity="+opacity+")";
	opacity -= 10;
	if(opacity>0) setTimeout("DoFadeOut()",50);
	else
	{
		document.getElementById("ErrorDialog").style.filter = "Alpha(Opacity=0)";
		document.getElementById("ErrorDialog").style.left = "-350px";
		document.getElementById("ErrorDialog").style.top = "-130px";
	}
}
function HideErrorDialog()
{
	opacity = 80;
	DoFadeOut();
}