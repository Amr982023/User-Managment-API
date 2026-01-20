# User Management API

A simple RESTful API for user management with JWT authentication.

---

## Base URL
https://localhost:5001/api/users

---

## Authentication (JWT)
- Login endpoint returns a JWT token
- All protected endpoints require the following header:

Authorization: Bearer {JWT_TOKEN}

---

## Endpoints

### 1. Register User
POST /register

Request Body:
{
    "username": "Ali",
    "email": "Ali@example.com",
    "userRole": 1,
    "password": "Ali1234#",
    "confirmPassword": "Ali1234#",
    "dateOfBirth": "1998-02-15T00:00:00Z"
}

Responses:
- 201 Created
- 400 Bad Request (validation error)

---

### 2. Login
POST /login

Request Body:
{
    "username": "amr.elbehedy",
    "password": "Amr1234#"  
}

Responses:
- 200 OK
{
  "token": "JWT_TOKEN"
}
- 401 Unauthorized

---

### 3. Get User By Id
GET /{id}

Authorization:
- Bearer token required

Responses:
- 200 OK
- 404 Not Found

---

### 4. Get All Users (Admin Only)
GET /

Authorization:
- Bearer token required
- Role: Admin

Response:
- 200 OK

---

### 5. Update User
PUT /{id}

Authorization:
- Bearer token required

Request Body:
{
  "username": "amr.elbehedy",
  "email": "amr.elbehedy@example.com",
  "password": "P@ssw0rd123!",
  "dateOfBirth": "1996-08-15T00:00:00Z",
  "role": 2
}

Responses:
- 204 No Content
- 404 Not Found

---

### 6. Delete User (Admin Only)
DELETE /{id}

Authorization:
- Bearer token required
- Role: Admin

Responses:
- 204 No Content
- 404 Not Found

---

## Testing
- Use Postman or any API testing tool
- Call /login to obtain a JWT token
- Add the token in the Authorization tab as Bearer Token
- Test protected endpoints

---

## Notes
- Passwords are stored securely using Hashing with Salt
- Input validation is applied (required fields, email format, password strength)
- Role-based authorization is implemented (User / Admin)
