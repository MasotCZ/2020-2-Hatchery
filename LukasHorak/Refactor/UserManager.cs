class User
{
  int Id { get; set; }
  string Email { get; set; }
}

interface IUserRepository {
  /// <summary>
  /// Creates user and returns his unique id.
  /// </summary>
  int Create(string email, string password);
  void Delete(int id);
}

class UserManager
{
  private IUserRepository _repository;

  public UserManager(IUserRepository repository)
  {
    _repository = repository;
  }

  User CreateUser(string email, string password) {
    var userId = _repository.Create(email, password);
    return new User { Id = userId, Email = email };
  }

  void DeleteUser(int id) {
    _repository.Delete(id);
  }

  // EditEmail
  // DeleteUser
  // ListUsers
}

class UserManagerTests {
  [Fact]
  public void TestUserCreation() {
    // Arrange
    var userRepository = Mock.Of<IUserRepository>(repository => repository.Create("e@h.cz", "heslo1235") == 222);
    var userManager = new UserManager(userRepository);

    // Act
    var user = userManager.CreateUser("e@h.cz", "heslo1235");

    // Assert
    user.Id.ShouldBe(222);
    user.Email.ShouldBe("e@h.cz");
  }
}

interface IDbConnection<_T>{
  void Add(_T en);
  void Del(_T en);
}
interface IConfigurationManager {
  string GetConnectionString(){}
}

class DbConn : IDbConnection{
  private readonly IConfigurationManager _config;

  DbConn(IConfigurationManager config){
    _config = config;    
  }  
}

class MsSqlUserRepository : IUserRepository
{
  private readonly IDbConnection _dbConnection;

  public MsSqlUserRepository(IDbConnection conn){
    _dbConnection = conn;
  }

  int Create(string email, string password)
  {
    var created = _dbConnection.Add();
    return created.Id;
  }

  void Delete(int userId)
  {
    var connectionString = ConfigurationManager.Get("ConnectionStrings").Get("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString)) {
      throw new SomeCustomException("Connection string is missing in app.config");
    }

    _dbConnection.Delete(userId);
  
  }

  // EditEmail
  // DeleteUser
  // ListUsers
}

class GoogleUserRepository : IUserRepository
{
  
}