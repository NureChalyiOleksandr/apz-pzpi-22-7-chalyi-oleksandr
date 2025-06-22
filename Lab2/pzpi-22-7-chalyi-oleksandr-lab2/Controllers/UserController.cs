using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace SmartLightSense.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UserUpdateDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(updateUserDto.UserName))
        {
            user.UserName = updateUserDto.UserName;
        }
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.Password = updateUserDto.Password;
        }
        if (!string.IsNullOrEmpty(updateUserDto.Role))
        {
            user.Role = updateUserDto.Role;
        }
        if (!string.IsNullOrEmpty(updateUserDto.Email))
        {
            user.Email = updateUserDto.Email;
        }
        if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
        {
            user.PhoneNumber = updateUserDto.PhoneNumber;
        }

        var updatedUser = await _userRepository.UpdateAsync(user);
 
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _userRepository.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
