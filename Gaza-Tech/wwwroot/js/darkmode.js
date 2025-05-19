const toggleBtn = document.getElementById("darkModeToggle");
const body = document.body;

// تحقق مما إذا كان المستخدم قد اختار الوضع الداكن مسبقًا
if (localStorage.getItem("dark-mode") === "enabled") {
    body.classList.add("dark-mode");
}

toggleBtn.addEventListener("click", () => {
    body.classList.toggle("dark-mode");

    // حفظ الاختيار في Local Storage
    if (body.classList.contains("dark-mode")) {
        localStorage.setItem("dark-mode", "enabled");
    } else {
        localStorage.setItem("dark-mode", "disabled");
    }
});
