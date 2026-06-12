function openHome() {
  window.location.href = "/Home/Index";
}

function openAbout() {
  window.location.href = "/Home/About";
}

function openProjects() {
  window.location.href = "/Home/Projects";
}

function openEvents() {
  window.location.href = "/Home/Events";
}

function openContact() {
  window.location.href = "/Home/Contact";
}

function openJoin_Us() {
  window.location.href = "/Home/Join_Us";
}

function openlogin() {
  window.location.href = "/Home/login";
}

function openEditProfile() {
  window.location.href = "/Home/EditProfile";
}

function openSaveChanges() {
  window.location.href = "/Home/MyProfile";
}

function openCancel() {
  window.location.href = "/Home/MyProfile";
}

document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("signupForm");

  // If this page is not signup page, skip
  if (!form) return;

  form.addEventListener("submit", async function (e) {
    e.preventDefault();

    const fullName = document.getElementById("FullName")?.value;
    const username = document.getElementById("Username")?.value;
    const email = document.getElementById("Email")?.value;
    const department = document.getElementById("Department")?.value;
    const batch = document.getElementById("BatchSession")?.value;
    const password = document.getElementById("PasswordHash")?.value;
    const confirmPassword = document.getElementById("ConfirmPassword")?.value;

    // Validation
    if (
      !fullName ||
      !username ||
      !email ||
      !department ||
      !batch ||
      !password
    ) {
      alert("Please fill all fields");
      // alert("fulname is " + batch);
      return;
    }

    if (password !== confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    const user = {
      FullName: fullName,
      Username: username,
      Email: email,
      Department: department,
      BatchSession: batch,
      PasswordHash: password,
    };

    try {
      const response = await fetch("/api/Users/registration", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(user),
      });

      const result = await response.text();

      alert(result);

      if (response.ok) {
        form.reset();
      }
    } catch (error) {
      console.error(error);
      alert("Server error. Please try again.");
    }
  });
});

document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  if (!loginForm) return;

  loginForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("Email").value;
    const password = document.getElementById("Password").value;

    if (!email || !password) {
      alert("Please enter email and password");
      return;
    }

    try {
      const response = await fetch("/api/Users/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          Email: email,
          Password: password,
        }),
      });

      const result = await response.json();

      console.log(result);

      if (result.success) {
        alert("Login Successful");
        window.location.href = "/Home/MyProfile";
      } else {
        alert(result.message);
      }
    } catch (error) {
      console.error(error);
      alert("Login failed");
    }
  });
});
