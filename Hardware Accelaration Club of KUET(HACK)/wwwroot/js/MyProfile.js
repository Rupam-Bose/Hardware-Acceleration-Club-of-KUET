document.addEventListener("DOMContentLoaded", loadProfile);

async function loadProfile() {
  try {
    const response = await fetch("/api/MyProfile");

    if (!response.ok) {
      throw new Error("Failed to fetch profile data");
    }

    const result = await response.json();

    if (!result.success) {
      console.error(result.message);
      return;
    }

    const user = result.data;

    // Profile Header
    document.getElementById("fullName").textContent = user.fullName;
    document.getElementById("email").textContent = user.email;
    document.getElementById("department").textContent = user.department;
    document.getElementById("batchSession").textContent = user.batchSession;
    document.getElementById("username").textContent = user.username;
  } catch (error) {
    console.error("Error loading profile:", error);
  }
}
