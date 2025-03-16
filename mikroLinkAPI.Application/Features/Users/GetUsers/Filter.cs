namespace mikroLinkAPI.Application.Features.Materials.GetMetarials;
public sealed record FilterUsers(Query<string> Name, Query<string> Surname, Query<string> Email, Query<int> Authority, Query<string> PhoneNumber, Query<string> OtherPhoneNumber);
