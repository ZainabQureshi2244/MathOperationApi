using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Erp_Repo.DTOs;
using ERP_dl.Data;
using ERP_dl.Entities;
using Erp_Repo.Interfaces;


namespace Erp_Repo.Interfaces
{
    // Interface
    public interface IMathService
    {
        Task<List<MathOperationResponseDto>> GetAllAsync();
        Task<MathOperationResponseDto> GetByIdAsync(int id);
        Task<MathOperationResponseDto> CreateAsync(CreateMathOperationDto dto);
        Task<MathOperationResponseDto> UpdateAsync(MathOperationUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

//Implementation
    public class MathOperationService : IMathService
{

    private readonly ApplicationDbContext _context;

    public MathOperationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MathOperationResponseDto>> GetAllAsync()
    {
        return await _context.Operations
            .Select(x => new MathOperationResponseDto
            {
                Id = x.Id,
                Number1 = x.Number1,
                Number2 = x.Number2,
                Result = x.Result,
                OperationType = x.OperationType,
                CreatedAt = x.CreatedAt
            }).ToListAsync();
    }

    public async Task<MathOperationResponseDto> GetByIdAsync(int id)
    {
        var entity = await _context.Operations.FindAsync(id);
        if (entity == null) return null;

        return new MathOperationResponseDto
        {
            Id = entity.Id,
            Number1 = entity.Number1,
            Number2 = entity.Number2,
            Result = entity.Result,
            OperationType = entity.OperationType,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task<MathOperationResponseDto> CreateAsync(CreateMathOperationDto dto)
    {
        double result = dto.OperationType switch
        {
            "Add" => dto.Number1 + dto.Number2,
            "Subtract" => dto.Number1 - dto.Number2,
            "Multiply" => dto.Number1 * dto.Number2,
            "Divide" when dto.Number2 != 0 => dto.Number1 / dto.Number2,
            _ => throw new ArgumentException("Invalid operation type or divide by zero.")
        };

        var entity = new Operation
        {
            Number1 = dto.Number1,
            Number2 = dto.Number2,
            Result = result,
            OperationType = dto.OperationType
        };

        _context.Operations.Add(entity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(entity.Id);
    }

    public async Task<MathOperationResponseDto> UpdateAsync(MathOperationUpdateDto dto)
    {
        var entity = await _context.Operations.FindAsync(dto.Id);
        if (entity == null) return null;

        entity.Number1 = dto.Number1;
        entity.Number2 = dto.Number2;
        entity.OperationType = dto.OperationType;
        entity.Result = dto.OperationType switch
        {
            "Add" => dto.Number1 + dto.Number2,
            "Subtract" => dto.Number1 - dto.Number2,
            "Multiply" => dto.Number1 * dto.Number2,
            "Divide" when dto.Number2 != 0 => dto.Number1 / dto.Number2,
            _ => throw new ArgumentException("Invalid operation type or divide by zero.")
        };

        await _context.SaveChangesAsync();

        return new MathOperationResponseDto
        {
            Id = entity.Id,
            Number1 = entity.Number1,
            Number2 = entity.Number2,
            OperationType = entity.OperationType,
            Result = entity.Result,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Operations.FindAsync(id);
        if (entity == null) return false;

        _context.Operations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
  }

