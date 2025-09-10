using EventSphere.Entity;

namespace EventSphere.Db;

public class EventSphereSeeder(EventSphereDbContext dbContext)
{
    public void Seed()
    {
        // Clear existing data
        dbContext.Database.EnsureCreated();

        // Check if data already exists
        if (dbContext.Users.Any())
        {
            return; // Database has been seeded
        }

        // Seed Venues
        var venues = new List<Venue>
        {
            new() { Name = "Main Auditorium", MaximumSeats = 500 },
            new() { Name = "Conference Hall A", MaximumSeats = 150 },
            new() { Name = "Conference Hall B", MaximumSeats = 100 },
            new() { Name = "Seminar Room 1", MaximumSeats = 50 },
            new() { Name = "Outdoor Amphitheater", MaximumSeats = 300 }
        };
        dbContext.Venues.AddRange(venues);
        dbContext.SaveChanges();

        // Seed Users (Admin, Organizers, Participants)
        var users = new List<User>
        {
            // Admin
            new()
            { 
                Email = "admin@eventsphere.com", 
                Password = "admin", 
                Role = UserRole.Admin, 
                CreatedAt = DateTime.Now.AddDays(-60) 
            },
            
            // Organizers
            new()
            { 
                Email = "organizer1@university.edu", 
                Password = "organizer1", 
                Role = UserRole.Organizer, 
                CreatedAt = DateTime.Now.AddDays(-45) 
            },
            new()
            { 
                Email = "organizer2@university.edu", 
                Password = "organizer2", 
                Role = UserRole.Organizer, 
                CreatedAt = DateTime.Now.AddDays(-40) 
            },
            
            // Participants
            new()
            { 
                Email = "john.doe@student.edu", 
                Password = "john", 
                Role = UserRole.Participant, 
                CreatedAt = DateTime.Now.AddDays(-30) 
            },
            new()
            { 
                Email = "jane.smith@student.edu", 
                Password = "jane", 
                Role = UserRole.Participant, 
                CreatedAt = DateTime.Now.AddDays(-25) 
            },
            new()
            { 
                Email = "mike.johnson@student.edu", 
                Password = "mike", 
                Role = UserRole.Participant, 
                CreatedAt = DateTime.Now.AddDays(-20) 
            },
            new()
            { 
                Email = "sarah.wilson@student.edu", 
                Password = "sarah", 
                Role = UserRole.Participant, 
                CreatedAt = DateTime.Now.AddDays(-15) 
            },
            new()
            { 
                Email = "alex.brown@student.edu", 
                Password = "alex", 
                Role = UserRole.Participant, 
                CreatedAt = DateTime.Now.AddDays(-10) 
            }
        };
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();

        // Seed UserDetails for all participants
        var userDetails = new List<UserDetail>
        {
            new()
            { 
                UserId = users[3].Id, // John Doe
                Name = "John Doe", 
                Phone = "1234567890", 
                Department = "Computer Science", 
                EnrollmentNo = "CS2021001" 
            },
            new()
            { 
                UserId = users[4].Id, // Jane Smith
                Name = "Jane Smith", 
                Phone = "1234567891", 
                Department = "Information Technology", 
                EnrollmentNo = "IT2021002" 
            },
            new()
            { 
                UserId = users[5].Id, // Mike Johnson
                Name = "Mike Johnson", 
                Phone = "1234567892", 
                Department = "Electronics Engineering", 
                EnrollmentNo = "EE2021003" 
            },
            new()
            { 
                UserId = users[6].Id, // Sarah Wilson
                Name = "Sarah Wilson", 
                Phone = "1234567893", 
                Department = "Business Administration", 
                EnrollmentNo = "BA2021004" 
            },
            new()
            { 
                UserId = users[7].Id, // Alex Brown
                Name = "Alex Brown", 
                Phone = "1234567894", 
                Department = "Mechanical Engineering", 
                EnrollmentNo = "ME2021005" 
            }
        };
        dbContext.UserDetails.AddRange(userDetails);
        dbContext.SaveChanges();

        // Seed Events
        var events = new List<Event>
        {
            new()
            {
                Title = "Tech Innovation Summit 2025",
                Description = "A comprehensive summit covering the latest trends in technology and innovation.",
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                Time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                TotalSeats = 200,
                VenueId = venues[0].Id,
                OrganizerId = users[1].Id, // organizer1
                WaitlistEnabled = true
            },
            new()
            {
                Title = "AI and Machine Learning Workshop",
                Description = "Hands-on workshop exploring artificial intelligence and machine learning concepts.",
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
                Time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14)),
                TotalSeats = 80,
                VenueId = venues[1].Id,
                OrganizerId = users[1].Id, // organizer1
                WaitlistEnabled = false
            },
            new()
            {
                Title = "Entrepreneurship Bootcamp",
                Description = "Learn the fundamentals of starting and running a successful business.",
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(45)),
                Time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10)),
                TotalSeats = 120,
                VenueId = venues[2].Id,
                OrganizerId = users[2].Id, // organizer2
                WaitlistEnabled = true
            },
            new()
            {
                Title = "Cybersecurity Awareness Seminar",
                Description = "Understanding modern cybersecurity threats and protection strategies.",
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(60)),
                Time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                TotalSeats = 40,
                VenueId = venues[3].Id,
                OrganizerId = users[2].Id, // organizer2
                WaitlistEnabled = false
            },
            new()
            {
                Title = "Annual Student Conference",
                Description = "A conference by students, for students, covering various academic disciplines.",
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(75)),
                Time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                TotalSeats = 250,
                VenueId = venues[4].Id,
                OrganizerId = users[1].Id, // organizer1
                WaitlistEnabled = true
            }
        };
        dbContext.Events.AddRange(events);
        dbContext.SaveChanges();

        // Seed Registrations
        var registrations = new List<Registration>
        {
            new()
            {
                EventId = events[0].Id,
                StudentId = users[3].Id, // John Doe
                RegisteredOn = DateTime.Now.AddDays(-10),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[0].Id,
                StudentId = users[4].Id, // Jane Smith
                RegisteredOn = DateTime.Now.AddDays(-9),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[1].Id,
                StudentId = users[3].Id, // John Doe
                RegisteredOn = DateTime.Now.AddDays(-8),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[1].Id,
                StudentId = users[5].Id, // Mike Johnson
                RegisteredOn = DateTime.Now.AddDays(-7),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[2].Id,
                StudentId = users[6].Id, // Sarah Wilson
                RegisteredOn = DateTime.Now.AddDays(-6),
                Status = RegistrationStatus.Waitlisted
            },
            new()
            {
                EventId = events[2].Id,
                StudentId = users[7].Id, // Alex Brown
                RegisteredOn = DateTime.Now.AddDays(-5),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[3].Id,
                StudentId = users[4].Id, // Jane Smith
                RegisteredOn = DateTime.Now.AddDays(-4),
                Status = RegistrationStatus.Confirmed
            },
            new()
            {
                EventId = events[4].Id,
                StudentId = users[5].Id, // Mike Johnson
                RegisteredOn = DateTime.Now.AddDays(-3),
                Status = RegistrationStatus.Cancelled
            }
        };
        dbContext.Registrations.AddRange(registrations);
        dbContext.SaveChanges();

        // Seed Attendance (for confirmed registrations)
        var attendances = new List<Attendance>
        {
            new()
            {
                RegistrationId = registrations[0].Id, // John Doe - Tech Summit
                Attended = true,
                MarkedOn = DateTime.Now.AddDays(-2)
            },
            new()
            {
                RegistrationId = registrations[1].Id, // Jane Smith - Tech Summit
                Attended = true,
                MarkedOn = DateTime.Now.AddDays(-2)
            },
            new()
            {
                RegistrationId = registrations[2].Id, // John Doe - AI Workshop
                Attended = false,
                MarkedOn = DateTime.Now.AddDays(-1)
            },
            new()
            {
                RegistrationId = registrations[3].Id, // Mike Johnson - AI Workshop
                Attended = true,
                MarkedOn = DateTime.Now.AddDays(-1)
            }
        };
        dbContext.Attendances.AddRange(attendances);
        dbContext.SaveChanges();

        // Seed Certificates (for attended events)
        var certificates = new List<Certificate>
        {
            new()
            {
                AttendanceId = attendances[0].RegistrationId, // John Doe - Tech Summit
                CertificateUrl = "https://certificates.eventsphere.com/tech-summit-2025/john-doe.pdf",
                IssuedOn = DateTime.Now.AddDays(-1)
            },
            new()
            {
                AttendanceId = attendances[1].RegistrationId, // Jane Smith - Tech Summit
                CertificateUrl = "https://certificates.eventsphere.com/tech-summit-2025/jane-smith.pdf",
                IssuedOn = DateTime.Now.AddDays(-1)
            },
            new()
            {
                AttendanceId = attendances[3].RegistrationId, // Mike Johnson - AI Workshop
                CertificateUrl = "https://certificates.eventsphere.com/ai-workshop/mike-johnson.pdf",
                IssuedOn = DateTime.Now
            }
        };
        dbContext.Certificates.AddRange(certificates);
        dbContext.SaveChanges();

        // Seed Feedback
        var feedbacks = new List<Feedback>
        {
            new()
            {
                RegistrationId = registrations[0].Id, // John Doe - Tech Summit
                Rating = 5,
                Comments = "Excellent event! Very informative and well-organized. The speakers were knowledgeable and engaging.",
                SubmittedOn = DateTime.Now.AddHours(-12)
            },
            new()
            {
                RegistrationId = registrations[1].Id, // Jane Smith - Tech Summit
                Rating = 4,
                Comments = "Great content and networking opportunities. The venue could have been a bit larger.",
                SubmittedOn = DateTime.Now.AddHours(-10)
            },
            new()
            {
                RegistrationId = registrations[3].Id, // Mike Johnson - AI Workshop
                Rating = 5,
                Comments = "Fantastic hands-on experience. Learned a lot about practical ML applications.",
                SubmittedOn = DateTime.Now.AddHours(-6)
            },
            new()
            {
                RegistrationId = registrations[6].Id, // Jane Smith - Cybersecurity Seminar
                Rating = 4,
                Comments = "Very relevant topic in today's digital world. Would recommend to others.",
                SubmittedOn = DateTime.Now.AddHours(-2)
            }
        };
        dbContext.Feedbacks.AddRange(feedbacks);
        dbContext.SaveChanges();

        // Seed Media Gallery
        var mediaGalleries = new List<MediaGallery>
        {
            new()
            {
                EventId = events[0].Id, // Tech Summit
                FileType = "image",
                FileUrl = "https://media.eventsphere.com/tech-summit-2025/opening-ceremony.jpg",
                UploadedBy = users[1].Id, // organizer1
                Caption = "Opening ceremony of Tech Innovation Summit 2025",
                UploadedOn = DateTime.Now.AddHours(-24)
            },
            new()
            {
                EventId = events[0].Id, // Tech Summit
                FileType = "video",
                FileUrl = "https://media.eventsphere.com/tech-summit-2025/keynote-speech.mp4",
                UploadedBy = users[1].Id, // organizer1
                Caption = "Keynote speech by industry expert",
                UploadedOn = DateTime.Now.AddHours(-20)
            },
            new()
            {
                EventId = events[1].Id, // AI Workshop
                FileType = "image",
                FileUrl = "https://media.eventsphere.com/ai-workshop/participants-working.jpg",
                UploadedBy = users[3].Id, // John Doe
                Caption = "Participants engaged in hands-on AI coding session",
                UploadedOn = DateTime.Now.AddHours(-18)
            },
            new()
            {
                EventId = events[2].Id, // Entrepreneurship Bootcamp
                FileType = "image",
                FileUrl = "https://media.eventsphere.com/entrepreneur-bootcamp/networking-session.jpg",
                UploadedBy = users[2].Id, // organizer2
                Caption = "Networking session during the bootcamp",
                UploadedOn = DateTime.Now.AddHours(-12)
            },
            new()
            {
                EventId = events[3].Id, // Cybersecurity Seminar
                FileType = "image",
                FileUrl = "https://media.eventsphere.com/cybersecurity-seminar/security-demo.jpg",
                UploadedBy = users[2].Id, // organizer2
                Caption = "Live demonstration of security vulnerabilities",
                UploadedOn = DateTime.Now.AddHours(-8)
            }
        };
        dbContext.MediaGalleries.AddRange(mediaGalleries);
        dbContext.SaveChanges();

        Console.WriteLine("Database seeded successfully!");
    }
}