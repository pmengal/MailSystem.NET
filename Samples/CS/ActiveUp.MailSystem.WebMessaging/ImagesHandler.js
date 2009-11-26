skinpath="icons/";

function createimg(arg) {
	if (document.images) {
		rslt = new Image();
		rslt.src = arg;
		return rslt;
	}
}

function rollimg() {
	if (document.images && (preimg == true)) {
		for (var i=0; i<rollimg.arguments.length; i+=2) {
			document[rollimg.arguments[i]].src = rollimg.arguments[i+1];
		}
	}
}

var preimg = false;
function preloadimg() {
	if (document.images) {
		compose_over = createimg(skinpath+"compose_on.gif");
		read_unread_over = createimg(skinpath+"read-unread-over.gif");
		mark_unmark_over = createimg(skinpath+"mark-unmark-over.gif");
		admin_folder_over = createimg(skinpath+"admin-folder-over.gif");
		search_over = createimg(skinpath+"search-over.gif");
		zip_over = createimg(skinpath+"zip-over.gif");
		delete_over = createimg(skinpath+"delete-over.gif");
		forward_over = createimg(skinpath+"forward-over.gif");
		reply_over = createimg(skinpath+"reply-over.gif");
		logout_over = createimg(skinpath+"logout-over.gif");
		error_over = createimg(skinpath+"error-over.gif");
		message_over = createimg(skinpath+"message-over.gif");
		into_folder_over = createimg(skinpath+"into-folder-over.gif");
		preimg = true;
	}
}
preloadimg();

var preimg = true;