document.addEventListener("DOMContentLoaded", () => {
  loadEditProfile();

  document
    .getElementById("profileForm")
    .addEventListener("submit", saveProfile);
});

async function loadEditProfile() {
  try {
    const response = await fetch("/api/ProfileApi");

    if (!response.ok) {
      throw new Error("Failed to load profile");
    }

    const result = await response.json();

    if (!result.success) {
      alert("Failed to load profile data");
      return;
    }

    const user = result.user;
    const academic = result.academicInfo;
    const profile = result.profileDetails;
    const social = result.socialLinks;

    // User Information

    document.getElementById("fullName").value = user?.fullName || "";

    document.getElementById("username").value = user?.username || "";

    document.getElementById("email").value = user?.email || "";

    document.getElementById("Department").value = user?.department || "";

    document.getElementById("Batch").value = user?.batchSession || "";

    // Academic Information

    document.getElementById("university").value = academic?.university || "";

    document.getElementById("semester").value = academic?.semester || "";

    // Profile Details

    document.getElementById("bio").value = profile?.bio || "";

    document.getElementById("about").value = profile?.about || "";

    // Social Links

    document.getElementById("linkedin").value = social?.linkedIn || "";

    document.getElementById("twitter").value = social?.twitter || "";

    document.getElementById("github").value = social?.gitHub || "";

    document.getElementById("website").value = social?.website || "";
  } catch (error) {
    console.error(error);

    alert("Error loading profile");
  }
}

async function saveProfile(e) {
  e.preventDefault();

  try {
    const profileData = {
      fullName: document.getElementById("fullName").value,

      username: document.getElementById("username").value,

      email: document.getElementById("email").value,

      department: document.getElementById("Department").value,

      batchSession: document.getElementById("Batch").value,

      university: document.getElementById("university").value,

      semester: document.getElementById("semester").value,

      bio: document.getElementById("bio").value,

      about: document.getElementById("about").value,

      linkedIn: document.getElementById("linkedin").value,

      twitter: document.getElementById("twitter").value,

      gitHub: document.getElementById("github").value,

      website: document.getElementById("website").value,
    };

    const response = await fetch("/api/ProfileApi", {
      method: "PUT",

      headers: {
        "Content-Type": "application/json",
      },

      body: JSON.stringify(profileData),
    });

    const result = await response.json();

    if (result.success) {
      alert("Profile Updated Successfully");

      window.location.href = "/Home/MyProfile";
    } else {
      alert(result.message || "Update Failed");
    }
  } catch (error) {
    console.error(error);

    alert("Server Error");
  }
}
