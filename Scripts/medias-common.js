// Shared functions used by medias views
function setCurrentPageLimit() {
    try {
        let nbColumns = 3; // at worse there will be 3 medias per row.
        let nbRows = 4; // default
        let pageSize = nbRows * nbColumns + nbColumns;
        let mediaLayout = $(".mediaLayout").first();
        let mediaDefaultOuterHeight = 240; // default

        if (mediaLayout != null) {
            if (mediaLayout.outerHeight() != undefined) {
                mediaDefaultOuterHeight = mediaLayout.outerHeight();
            }
            let nbRowsCalc = Math.round($("#mainContentPanel").innerHeight() / mediaDefaultOuterHeight);
            pageSize = nbRowsCalc * nbColumns + nbColumns;
        }
        $.ajax({ url: `/Medias/SetFirstPageSize?pageSize=${pageSize}` });
    } catch (e) {
        console && console.warn && console.warn('setCurrentPageLimit failed', e);
    }
}
