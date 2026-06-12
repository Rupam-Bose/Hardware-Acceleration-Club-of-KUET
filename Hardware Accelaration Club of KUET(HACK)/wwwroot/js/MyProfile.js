document.addEventListener("DOMContentLoaded", loadProfile);

async function loadProfile() {
  try {
    const response = await fetch("/api/ProfileApi");

    if (!response.ok) {
      throw new Error("Failed to load profile");
    }

    const result = await response.json();

    if (!result.success) {
      throw new Error("Profile data not found");
    }

    const user = result.user;
    const academic = result.academicInfo;
    const profile = result.profileDetails;
    const social = result.socialLinks;

    document.getElementById("fullName").innerText = user.fullName || "N/A";

    document.getElementById("department").innerText = user.department || "N/A";

    document.getElementById("batchSession").innerText =
      user.batchSession || "N/A";

    document.getElementById("email").innerText = user.email || "N/A";

    const university = document.getElementById("university");

    if (university) {
      university.innerText = academic.university || "N/A";
    }

    const bio = document.querySelector(".bio");

    if (bio) {
      bio.innerText = profile.bio || "No bio available";
    }

    const about = document.getElementById("aboutMe");

    if (about) {
      about.innerText = profile.about || "No information available";
    }

    const website = document.getElementById("website");

    if (website) {
      website.innerText = profile.website || social.website || "N/A";
    }

    const websiteInfo = document.getElementById("websiteInfo");

    if (websiteInfo) {
      websiteInfo.innerText = profile.website || social.website || "N/A";
    }

    const dob = document.getElementById("dateOfBirth");

    if (dob) {
      if (profile.dateOfBirth) {
        const date = new Date(profile.dateOfBirth);

        dob.innerText = date.toLocaleDateString();
      } else {
        dob.innerText = "N/A";
      }
    }

    const profileImage = document.getElementById("profileImage");

    if (
      profileImage &&
      profile.profileImage &&
      profile.profileImage.trim() !== ""
    ) {
      profileImage.src = profile.profileImage;
    }

    console.log("LinkedIn:", social.linkedIn);

    console.log("Twitter:", social.twitter);

    console.log("GitHub:", social.gitHub);

    console.log("Portfolio:", social.website);
  } catch (error) {
    console.error("Error loading profile:", error);
  }
}
