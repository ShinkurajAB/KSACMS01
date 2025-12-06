//-------------User Details Drop down------------------
function showUserDetailDrodown(){
    
const menu = document.getElementById("userDetailMenuBox");

  if (menu.classList.contains("show")) {
    menu.classList.remove("show");
    menu.classList.add("hide");
  } else {
    menu.classList.remove("hide");
    menu.classList.add("show");
  }

}

//document.body.addEventListener("click", function (e) {
//  const menu = document.getElementById("userDetailMenuBox");
//  const btn = document.getElementById("dropdownBtn");

//  if (!btn.contains(e.target) && !menu.contains(e.target)) {
//    menu.classList.remove("show");
//    menu.classList.add("hide");
//  }
//});


//-------------Pop Up-----------------
function openPopup() {
 const Modal = document.getElementById("LogoutPopUp");
 const popup = document.getElementById("ModalContaner");
  Modal.classList.remove("hidden");
  popup.classList.remove("popup-animate");
  void popup.offsetWidth;    
  popup.classList.add("popup-animate");
}

// CLOSE POPUP
function closePopup() {
 const Modal = document.getElementById("LogoutPopUp");
  Modal.classList.add("hidden");
}
//--------------------------------------
//-------------Pop Up-----------------
function showEditCompanyManagementModal() {
 const Modal = document.getElementById("EditCompanyManagementModal");
 const popup = document.getElementById("ModalContaner");
  Modal.classList.remove("hidden");
  popup.classList.remove("popup-animate");
  void popup.offsetWidth;    
  popup.classList.add("popup-animate");
}

// CLOSE POPUP
function hideEditCompanyManagementModal() {
    const Modal = document.getElementById("EditCompanyManagementModal");
  Modal.classList.add("hidden");
}
//--------------------------------------
//-------------Pop Up-----------------
function showDeleteConfirmModal() {
    const Modal = document.getElementById("DeleteConfirmModal");
    const popup = document.getElementById("ModalContaner");
    Modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");
}

// CLOSE POPUP
function closeDeleteConfirmModal() {
    const Modal = document.getElementById("DeleteConfirmModal");
    Modal.classList.add("hidden");
}
//--------------------------------------
//-------------View Attachment Modal-----------------
function showViewAttachmentModal() {
    const Modal = document.getElementById("ViewAttachmentModal");
    const popup = document.getElementById("AttachmentModalContaner");
    Modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");
}
// CLOSE POPUP
function hideViewAttachmentModal() {
    const Modal = document.getElementById("ViewAttachmentModal");
    Modal.classList.add("hidden");
}
//-------------Pop Up-----------------
function showCreateNewDataPopUP() {
 const Modal = document.getElementById("CreateNewDataPopup");
 const popup = document.getElementById("ModalContaner");
  Modal.classList.remove("hidden");
  popup.classList.remove("popup-animate");
  void popup.offsetWidth;    
  popup.classList.add("popup-animate");
}

// CLOSE POPUP
function hideCreateNewDataPopUP() {
 const Modal = document.getElementById("CreateNewDataPopup");
  Modal.classList.add("hidden");
}
//--------------------------------------
//-------------Edit Employee Modal-----------------
function showEditEmployeeModal() {
    const Modal = document.getElementById("EditEmployeeModal");
    const popup = document.getElementById("ModalContaner");
    Modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");
}
// CLOSE POPUP
function hideEditEmployeeModal() {
    const Modal = document.getElementById("EditEmployeeModal");
    Modal.classList.add("hidden");
}
//--------------------------------------
//------------Side Menu-----------------
function toggleMenu() {
  const clicked = event.currentTarget;     
  const submenu = clicked.nextElementSibling; 
  submenu.classList.toggle("open");
}

//-------------------Three Dot Sidemenu------------------
function toggleThreeDotMenu() {
  // detect clicked button
  const btn = event.target.closest(".three-btn");

  // dropdown next to button
  const menu = btn.nextElementSibling;

  // close all first
  document.querySelectorAll(".dropdown").forEach(m => {
    m.classList.add("hidden");
    m.style.opacity = "0";
    m.style.transform = "scale(0.95)";
  });

  // toggle only this one
  if (menu.classList.contains("hidden")) {
    menu.classList.remove("hidden");
    setTimeout(() => {
      menu.style.opacity = "1";
      menu.style.transform = "scale(1)";
    }, 10);
  }
}

// close when clicking outside
document.addEventListener("click", function (e) {
  if (!e.target.closest(".three-btn") && !e.target.closest(".dropdown")) {
    document.querySelectorAll(".dropdown").forEach(m => {
      m.classList.add("hidden");
      m.style.opacity = "0";
      m.style.transform = "scale(0.95)";
    });
  }
});


// -----Create Update User Modal Popup
function UseropenModal() {
    const Modal = document.getElementById("CreateUser");
    const popup = document.getElementById("ModalContaner");
    Modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");
}

function UsercloseModal() {
    const Modal = document.getElementById("CreateUser");
    Modal.classList.add("hidden");
}
//------------Accordion-------------
window.attachmentAccordion = function () {
    document.querySelectorAll("[data-attachment-accordion]").forEach(root => {
        if (root._attachmentAccordionInit) return;
        root._attachmentAccordionInit = true;

        const buttons = Array.from(root.querySelectorAll(".acc-btn"));

        buttons.forEach(btn => {
            const panel = btn.nextElementSibling;
            panel.style.overflow = "hidden";
            panel.style.maxHeight = panel.classList.contains("hidden") ? "0px" : (panel.scrollHeight + 20) + "px";
            panel.style.transition = "max-height 350ms ease, opacity 250ms linear, padding 250ms linear";
            panel.style.opacity = panel.classList.contains("hidden") ? "0" : "1";
            if (panel.classList.contains("hidden")) {
                panel.style.maxHeight = "0px";
                panel.style.paddingTop = "0px";
                panel.style.paddingBottom = "0px";
            } else {
                panel.style.paddingTop = ""; 
                panel.style.paddingBottom = "";
            }

            btn.addEventListener("click", () => {
                const isHidden = panel.classList.contains("hidden");

                if (isHidden) {
                    panel.classList.remove("hidden");
                    panel.style.paddingTop = "";
                    panel.style.paddingBottom = "";
                    const h = panel.scrollHeight + 20;
                    panel.style.maxHeight = h + "px";
                    panel.style.opacity = "1";
                    const sym = btn.querySelector(".acc-symbol");
                    if (sym) sym.style.transform = "rotate(45deg)"; 
                } else {
                    panel.style.maxHeight = "0px";
                    panel.style.opacity = "0";
                    panel.style.paddingTop = "0px";
                    panel.style.paddingBottom = "0px";
                    setTimeout(() => {

                        panel.classList.add("hidden");
                    }, 360); 
                    const sym = btn.querySelector(".acc-symbol");
                    if (sym) sym.style.transform = "rotate(0deg)";
                }
            });

            const sym = btn.querySelector(".acc-symbol");
            if (sym) {
                sym.style.transition = "transform 250ms ease";
                sym.style.display = "inline-block";
                sym.style.transform = "rotate(0deg)";
            }
        });
    });
};

// Create Advatise Modal

function CreateAdvatiseModal() {

    const Modal = document.getElementById("addAdModal");
    const popup = document.getElementById("ModalContaner");
    Modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");

     
}

function HideAdvatiseModal() {
    const Modal = document.getElementById("addAdModal");
    Modal.classList.add("hidden");

    
}



//Download 
function DownLoadFile(mimeType, Base64String, fileName) {
    var fileDataUrl = "data:" + mimeType + "; base64," + Base64String;
    fetch(fileDataUrl).then(response => response.blob())
        .then(blob => {
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });

}



// Document Modal
function CreateDocumentModal() {
    const modal = document.getElementById("addDocModal");
    const popup = document.getElementById("ModalContaner");
    modal.classList.remove("hidden");
    popup.classList.remove("popup-animate");
    void popup.offsetWidth;
    popup.classList.add("popup-animate");
}

function HideDocumentModal() {
    const Modal = document.getElementById("addDocModal");
    Modal.classList.add("hidden");
}