function openWin(pageURL, sizeW, sizeH)
{
	centerHeight = (screen.availHeight - sizeH)/2;
	centerWidth  = (screen.availWidth - sizeW)/2; 
	winParam = "top="+ centerHeight +", left="+ centerWidth +",  scrollbars=no, resizable=yes, width=" + sizeW + ",height=" + sizeH;
	window.open(pageURL,'', winParam);
}