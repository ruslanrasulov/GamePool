;"use strict";

(function () {
    var $removeButtons = $(".remove-user-role"),
        $addButtons = $(".add-user-role");

    $addButtons.on("click", function (e) {
        var $target = $(e.target),
            $prevRolesSelect = $target.prev(),
            currRole = $prevRolesSelect.val(),
            userName = $prevRolesSelect.data("user-name"),
            $roleList = $target.parent().prev(),
            roles = $roleList.text();

        if (currRole) {
            $.get("/Admin/AddRoleToUser", { userName: userName, roleName: currRole }, function (data) {
                var $selectedOption = $prevRolesSelect.children("option[value=" + currRole + "]"),
                    $currRolesSelect = $target.next();

                if (data) {
                    $currRolesSelect.append($selectedOption);
                    $prevRolesSelect.remove("option[value=" + currRole + "]");

                    console.log(roles);
                    if (roles !== "") {
                        $roleList.text(roles + "," + currRole);
                    } else {
                        $roleList.text(currRole);
                    }
                }
            });
        }
    });

    $removeButtons.on("click", function (e) {
        var $target = $(e.target),
            $prevRolesSelect = $target.prev(),
            currRole = $prevRolesSelect.val(),
            userName = $prevRolesSelect.data("user-name"),
            $roleList = $target.parent().prev(),
            roles = $roleList.text();

        if (currRole) {
            $.get("/Admin/RemoveRoleFromUser", { userName: userName, roleName: currRole }, function (data) {
                var $selectedOption = $prevRolesSelect.children("option[value=" + currRole + "]"),
                    $currRolesSelect = $prevRolesSelect.prev().prev();

                if (data) {
                    $currRolesSelect.append($selectedOption);
                    $prevRolesSelect.remove("option[value=" + currRole + "]");

                    $roleList.text(roles.replace(new RegExp(",?\s*" + currRole + ",?\s*"), ""));
                }
            });
        }
    });
})();