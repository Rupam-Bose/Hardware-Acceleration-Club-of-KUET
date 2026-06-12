document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("contactForm");

  if (form) {
    form.addEventListener("submit", sendMessage);
  }
});

async function sendMessage(e) {
  e.preventDefault();

  const data = {
    fullName: document.getElementById("fullName").value,

    email: document.getElementById("email").value,

    subject: document.getElementById("subject").value,

    message: document.getElementById("message").value,
  };

  try {
    const response = await fetch("/api/ContactApi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

    const result = await response.json();

    if (result.success) {
      alert("Message sent successfully!");

      document.getElementById("contactForm").reset();
    } else {
      alert(result.message);
    }
  } catch (error) {
    console.error(error);

    alert("Failed to send message.");
  }
}
